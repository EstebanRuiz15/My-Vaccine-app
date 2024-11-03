using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Categories;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<VaccineCategory> _categoriesRpo;
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<VaccineCategory> categories, IMapper mapper)
        {
            _categoriesRpo = categories;
            _mapper = mapper;
        }
        public async Task<CategoriResponseDto> Add(CategoriRequestDto request)
        {
            var Categories = new VaccineCategory();
            Categories.Name = request.Name;

            await _categoriesRpo.Add(Categories);
            var response = _mapper.Map<CategoriResponseDto>(Categories);

            return response;
        }

        public async Task<CategoriResponseDto> Delete(int id)
        {
            var Categories = await _categoriesRpo.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
            await _categoriesRpo.Delete(Categories);
            var response = _mapper.Map<CategoriResponseDto>(Categories);

            return response;
        }

        public async Task<IEnumerable<CategoriResponseDto>> GetAll()
        {
            var Categories = await _categoriesRpo.GetAll().ToListAsync();
            var response = _mapper.Map<IEnumerable <CategoriResponseDto>>(Categories);

            return response;
        }

        public async Task<IEnumerable<VaccineCategory>> GetAllCategories()
        {
            var categories = await _categoriesRpo.GetAll().ToListAsync(); 
            return categories;
        }

        public async Task<CategoriResponseDto> GetById(int id)
        {
            var Categories = await _categoriesRpo.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<CategoriResponseDto>(Categories);
            return response;
        }

        public async Task<CategoriResponseDto> Update(CategoriRequestDto request, int id)
        {
            var Categories = await _categoriesRpo.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
            Categories.Name=request.Name;
            await _categoriesRpo.Update(Categories);
            var response = _mapper.Map<CategoriResponseDto>(Categories);
            return response;
        }
    }
}

