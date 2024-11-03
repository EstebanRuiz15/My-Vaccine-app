using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.Vacccine;

namespace My_vaccine_app.Services.Contracts
{
    public interface IVaccineService
    {
        Task<IEnumerable<VaccineResponseDto>> GetAll();
        Task<VaccineResponseDto> GetById(int id);
        Task<VaccineResponseDto> Update(VaccineRequestDto request, int id);
        Task<VaccineResponseDto> Delete(int id);
        Task<VaccineResponseDto> Add(VaccineRequestDto request);
    }
}
