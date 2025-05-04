using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Categories;
using My_vaccine_app.Models;

namespace My_vaccine_app.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoriResponseDto>> GetAll();
        Task<CategoriResponseDto> GetById(int id);
        Task<CategoriResponseDto> Update(CategoriRequestDto request, int id);
        Task<CategoriResponseDto> Delete(int id);
        Task<CategoriResponseDto> Add(CategoriRequestDto request);
        Task<IEnumerable<VaccineCategory>> GetAllCategories();
    }
}
