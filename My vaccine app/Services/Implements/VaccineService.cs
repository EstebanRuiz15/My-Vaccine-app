using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Services.Implements
{
    public class VaccineService : IVaccineService
    {
        private readonly IBaseRepository<Vaccine> _VaccineRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public VaccineService(IBaseRepository<Vaccine> VaccineRepo, IMapper mapper, ICategoryService categoryService)
        {
            _VaccineRepo = VaccineRepo;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<VaccineResponseDto> Add(VaccineRequestDto request)
        {
            var existingCategories = await _categoryService.GetAllCategories();

            var validCategories = new List<VaccineCategory>();
            var invalidCategories = new List<string>();

            foreach (var categoryName in request.Categories)
            {
                var category = existingCategories
                    .FirstOrDefault(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));

                if (category == null)
                    invalidCategories.Add(categoryName);
                else
                    validCategories.Add(category);
            }

            if (invalidCategories.Any())
            {
                return null;
            }
            var vaccine = _mapper.Map<Vaccine>(request);
            vaccine.Categories = validCategories;
            await _VaccineRepo.Add(vaccine);
            return _mapper.Map<VaccineResponseDto>(vaccine);
        }

        public async  Task<VaccineResponseDto> Delete(int id)
        {
            var Vaccine = await _VaccineRepo.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();

            await _VaccineRepo.Delete(Vaccine);
            var response = _mapper.Map<VaccineResponseDto>(Vaccine);

            return response;
        }

        public  async Task<IEnumerable<VaccineResponseDto>> GetAll()
        {
            var Vaccine = await _VaccineRepo.GetAll().Include(c => c.Categories).AsNoTracking().ToListAsync();

            var response = _mapper.Map<IEnumerable <VaccineResponseDto>>(Vaccine);

            return response;
        }

        public async Task<VaccineResponseDto> GetById(int id)
        {
            var Vaccine = await _VaccineRepo.FindBy(x => x.VaccineId == id).Include(c => c.Categories) .FirstOrDefaultAsync();
            var response = _mapper.Map<VaccineResponseDto>(Vaccine);

            return response;
        }


        public async Task<VaccineResponseDto> Update(VaccineRequestDto request, int id)
        {
            var vaccine = await _VaccineRepo.FindBy(x => x.VaccineId == id)
         .Include(v => v.Categories) 
         .FirstOrDefaultAsync();

            if (vaccine == null)
            {
                return null;
            }

            var existingCategories = await _categoryService.GetAllCategories();
            var validCategories = new List<VaccineCategory>();
            var invalidCategories = new List<string>();

            foreach (var categoryName in request.Categories)
            {
                var category = existingCategories
                    .FirstOrDefault(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));
                if (category == null)
                    invalidCategories.Add(categoryName);
                else
                    validCategories.Add(category);
            }

            if (invalidCategories.Any())
            {
                return null;
            }

            vaccine.Name = request.Name;
            vaccine.RequiresBooster = request.RequieredBooster;

            vaccine.Categories.Clear();
            foreach (var category in validCategories)
            {
                vaccine.Categories.Add(category);
            }

            await _VaccineRepo.Update(vaccine);
            return _mapper.Map<VaccineResponseDto>(vaccine);
        }
    }
}
