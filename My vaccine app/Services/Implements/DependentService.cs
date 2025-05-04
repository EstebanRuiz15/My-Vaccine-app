using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Services.Implements
{
    public class DependentService : IDependentService

    {
        private readonly IBaseRepository<Dependent> _dependentRepo;
        private readonly IMapper _mapper;
        public DependentService(IBaseRepository<Dependent> dependentRepo, IMapper mapper)
        {
            _dependentRepo = dependentRepo;
            _mapper = mapper;
        }
        public async Task<DependentResponseDto> Add(DependentRequestDto request)
        {
            var dependents = new Dependent();
            dependents.Name = request.Name;
            dependents.DateOfBirth = request.DateOfBirth;
            dependents.UserId = request.UserId;

            await _dependentRepo.Add(dependents);
            var response = _mapper.Map<DependentResponseDto>(dependents);

            return response;
        }

        public async Task<DependentResponseDto> Delete(int id)
        {
            var dependents = await _dependentRepo.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();

            await _dependentRepo.Delete(dependents);
            var response = _mapper.Map<DependentResponseDto>(dependents);

            return response;
        }

        public async Task<IEnumerable <DependentResponseDto>> GetAll()
        {
            var dependents =await _dependentRepo.GetAll().AsNoTracking().ToListAsync();
            var response =_mapper.Map<IEnumerable <DependentResponseDto>>(dependents);
            return response;
        }

        public  async Task<DependentResponseDto> GetById(int id)
        {
            var dependents = await _dependentRepo.FindByAsNoTracking(x => x.DependentId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<DependentResponseDto>(dependents);
            return response;
        }

        public async Task<IEnumerable<DependentResponseDto>> GetByUserID(int id)
        {
            var dependents = await _dependentRepo.FindBy(x => x.UserId == id).ToListAsync();
            var response = _mapper.Map<IEnumerable <DependentResponseDto>>(dependents);
            return response;
        }

        public async  Task<DependentResponseDto> Update(DependentRequestDto request, int id)
        {
            var dependents = await _dependentRepo.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
            dependents.Name = request.Name;
            dependents.DateOfBirth = request.DateOfBirth;
            dependents.UserId = request.UserId;

            await _dependentRepo.Update(dependents);
            var response = _mapper.Map<DependentResponseDto>(dependents);

            return response;
        }
    }
}
