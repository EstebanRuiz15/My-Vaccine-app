using My_vaccine_app.Dtos.Dependent;

namespace My_vaccine_app.Services.Contracts
{
    public interface IDependentService
    {
        Task<IEnumerable <DependentResponseDto>> GetAll();
        Task<IEnumerable<DependentResponseDto>> GetByUserID(int id);
        Task<DependentResponseDto> GetById(int id);
        Task<DependentResponseDto> Update(DependentRequestDto request, int id);
        Task<DependentResponseDto> Delete(int id);
        Task<DependentResponseDto> Add(DependentRequestDto request);

    }
}
