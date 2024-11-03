using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Dtos.VaccineRecord;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Services.Implements
{
    public class RecordService : IRecordService
    {
        private readonly IBaseRepository<VaccineRecord> _recordRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IVaccineService _vaccineService;
        public RecordService(IBaseRepository<VaccineRecord> recordRepo, IMapper mapper, IUserService userService, IVaccineService vaccineService)
        {
            _recordRepo = recordRepo;
            _mapper = mapper;
            _userService = userService;
            _vaccineService = vaccineService;
        }
        public async Task<RecordResponseDto> Add(RecordRequestDto request)
        {
            var existingUser = await _userService.GetById(request.UserId);
            if (existingUser == null) return null;
            var validVaccine = await _vaccineService.GetById(request.VaccineId);
            if (validVaccine == null) return null;

            var record = new VaccineRecord
            {
                UserId = request.UserId,
                VaccineId = request.VaccineId,
                DateAdministered = DateTime.Now,
                DependentId = request.DependentId,
                AdministeredLocation = request.AdministeredLocation,
                AdministeredBy = request.AdministeredBy,
            };
            await _recordRepo.Add(record);
            var response = await _recordRepo.FindBy(x => x.VaccineRecordId == record.VaccineRecordId).FirstOrDefaultAsync();
            return _mapper.Map<RecordResponseDto>(response);
        }

        public async Task<RecordResponseDto> Delete(int id)
        {
            var record= await _recordRepo.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();
            if (record == null) return null;
            await _recordRepo.Delete(record);
            return _mapper.Map<RecordResponseDto>(record);
        }

        public async Task<IEnumerable<RecordResponseDto>> GetAll()
        {
            var response = await _recordRepo.GetAll()
                .Include(x => x.Vaccine)
                    .ThenInclude(v => v.Categories)
                .Include(x => x.Dependent)
                .Include(x => x.User)
                    .ThenInclude(u => u.Allergies)
                .Include(x => x.User)
                .ToListAsync();
            return _mapper.Map<IEnumerable <RecordResponseDto>>(response);
        }

        public async Task<IEnumerable<RecordResponseDto>> GetByCurrentUser(string email)
        {
            var user= await _userService.GetUserInfo(email);
            int Userid= user.UserId;
            var records = await _recordRepo.FindBy(x => x.UserId == Userid).ToListAsync();
            return _mapper.Map<IEnumerable<RecordResponseDto>>(records);
        }

        public async Task<IEnumerable<RecordResponseDto>> GetByUserID(int id)
        {
           var user = await _userService.GetById(id);
            if (user == null) return null;
            var response = await _recordRepo.FindBy(x =>x.UserId == user.UserId).ToListAsync();
            return _mapper.Map<IEnumerable<RecordResponseDto>>(response);
        }
    }
}
