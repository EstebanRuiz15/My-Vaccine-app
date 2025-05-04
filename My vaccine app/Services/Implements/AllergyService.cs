using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;
using AllergyRequestDto = My_vaccine_app.Dtos.Allergies.AllergyRequestDto;

namespace My_vaccine_app.Services.Implements
{
    public class AllergyService : IAllergyService
    {
        private readonly IBaseRepository<Allergy> _allergytRepo;
        private readonly IMapper _mapper;

        public AllergyService(IBaseRepository<Allergy> allergytRepo,  IMapper mapper)
        {
            _allergytRepo = allergytRepo;
            _mapper = mapper;

        }
        public async Task<AllergyResponseDto> Add(AllergyRequestDto request)
        {
            var allergyes = new Allergy();
            allergyes.Name = request.Name;
            allergyes.UserId = request.UserId;

            await _allergytRepo.Add(allergyes);
            var response = _mapper.Map<AllergyResponseDto>(allergyes);

            return response;
        }

        public async Task<AllergyResponseDto> Delete(int id)
        {

            var allergy = await _allergytRepo.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();

            await _allergytRepo.Delete(allergy);
            var response = _mapper.Map<AllergyResponseDto>(allergy);

            return response;
        }

        public async Task<IEnumerable<AllergyResponseDto>> GetAll()
        {
            var allergies = await _allergytRepo.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<AllergyResponseDto>>(allergies);
            return response;
        }

        public async Task<AllergyResponseDto> GetById(int id)
        {
            var allergy = await _allergytRepo.FindByAsNoTracking(x => x.AllergyId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<AllergyResponseDto>(allergy);

            return response;
        }

        public async Task<IEnumerable<AllergyResponseDto>> GetByUserID(int id)
        {
            var allergy = await _allergytRepo.FindBy(x => x.UserId == id).ToListAsync();
            var response = _mapper.Map<IEnumerable <AllergyResponseDto>>(allergy);

            return response;
        }

        public async Task<AllergyResponseDto> Update(AllergyRequestDto request, int id)
        {
            var allergy = await _allergytRepo.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();
            allergy.Name = request.Name;
            allergy.UserId = request.UserId;

            await _allergytRepo.Update(allergy);
            var response = _mapper.Map<AllergyResponseDto>(allergy);

            return response;
        }
    }
}
