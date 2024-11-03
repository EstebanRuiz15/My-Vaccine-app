using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.FamilyGroup;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Services.Implements
{
    public class FamilyGroupService: IFamilyGroupService
    {
        private readonly IBaseRepository<FamilyGroup> _familyRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public FamilyGroupService(IBaseRepository<FamilyGroup> familyRepo, IMapper mapper, IUserService userService)
        {
            _familyRepo = familyRepo;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<GroupResponseDto> Add(GroupRequestDto request)
        {
            var validUsers = new List<User>();
            if (request.Users != null) 
            { 
            var existingUsers = await _userService.GetAll();
            var invalidUsers = new List<int>();

            foreach (var User in request.Users)
            {
                var user = existingUsers
                    .FirstOrDefault(c => (c.UserId == User));

                if (user == null)
                    invalidUsers.Add(User);
                else
                    validUsers.Add(user);
            }


            if (invalidUsers.Any())
            {
                return null;
            }
            }
            var family = _mapper.Map<FamilyGroup>(request);
            if(validUsers != null) family.Users = validUsers;
            await _familyRepo.Add(family);
            return _mapper.Map<GroupResponseDto>(family);
        }

        public async Task<GroupResponseDto> Delete(int id)
        {
            var family = await _familyRepo.FindBy(x => x.FamilyGroupId== id).FirstOrDefaultAsync();

            await _familyRepo.Delete(family);
            var response = _mapper.Map<GroupResponseDto>(family);

            return response;
        }

        public async Task<IEnumerable<GroupResponseDto>> GetAll()
        {
            var family = await _familyRepo.GetAll().Include(x=> x.Users).ToListAsync();
            var response = _mapper.Map<IEnumerable<GroupResponseDto>>(family);
            return response;
        }

        public async Task<GroupResponseDto> GetById(int id)
        {
            var family = await _familyRepo.FindBy(x => x.FamilyGroupId == id).Include(x=>x.Users) .FirstOrDefaultAsync();
            var response = _mapper.Map<GroupResponseDto>(family);
            return response;
        }

        public async Task<IEnumerable<GroupResponseDto>> GetByUserID(int id)
        {
            var user = await _userService.GetById(id) ;
            if (user == null) return null;
            if (user.FamilyGroups == null || !user.FamilyGroups.Any())
            {
                return null;
            }
            var family = await _familyRepo.FindBy(x => x.FamilyGroupId == user.FamilyGroups.First().FamilyGroupId).Include(x => x.Users) .ToListAsync();
            var response = _mapper.Map<IEnumerable<GroupResponseDto>>(family);
            return response;
        }

        public async Task<GroupResponseDto> Update(GroupRequestDto request, int id)
        {
            var family = await _familyRepo.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();
            if (family == null) return null;

            var existingUsers = await _userService.GetAll();

            var validUsers = new List<User>();
            if (request.Users != null)
            {
                var invalidUsers = new List<int>();

                foreach (var User in request.Users)
                {
                    var user = existingUsers
                        .FirstOrDefault(c => (c.UserId == User));

                    if (user == null)
                        invalidUsers.Add(User);
                    else
                        validUsers.Add(user);
                }


                if (invalidUsers.Any())
                {
                    return null;
                }
            }
            family.Name = request.Name;
            if (validUsers != null)family.Users = validUsers;
            await _familyRepo.Update(family);
            return _mapper.Map<GroupResponseDto>(family);
        }
    }
}
