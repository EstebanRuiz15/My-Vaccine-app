using My_vaccine_app.Dtos.FamilyGroup;

namespace My_vaccine_app.Services.Contracts
{
    public interface IFamilyGroupService
    {
        Task<IEnumerable<GroupResponseDto>> GetAll();
        Task<IEnumerable<GroupResponseDto>> GetByUserID(int id);
        Task<GroupResponseDto> GetById(int id);
        Task<GroupResponseDto> Update(GroupRequestDto request, int id);
        Task<GroupResponseDto> Delete(int id);
        Task<GroupResponseDto> Add(GroupRequestDto request);
    }
}
