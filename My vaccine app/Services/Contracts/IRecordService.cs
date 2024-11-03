using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.VaccineRecord;

namespace My_vaccine_app.Services.Contracts
{
    public interface IRecordService
    {
        Task<IEnumerable<RecordResponseDto>> GetAll();
        Task<IEnumerable<RecordResponseDto>> GetByUserID(int id);
        Task<IEnumerable <RecordResponseDto>> GetByCurrentUser(string email);
        Task<RecordResponseDto> Delete(int id);
        Task<RecordResponseDto> Add(RecordRequestDto request);
    }
}
