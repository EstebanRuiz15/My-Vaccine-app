using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Dependent;

namespace My_vaccine_app.Services.Contracts
{
    public interface IAllergyService
    {
        Task<IEnumerable<AllergyResponseDto>> GetAll();
        Task<IEnumerable<AllergyResponseDto>> GetByUserID(int id);
        Task<AllergyResponseDto> GetById(int id);
        Task<AllergyResponseDto> Update(AllergyRequestDto request, int id);
        Task<AllergyResponseDto> Delete(int id);
        Task<AllergyResponseDto> Add(AllergyRequestDto request);
    }
}
