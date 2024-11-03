using My_vaccine_app.Models;

namespace My_vaccine_app.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable <User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Delete(int id);
        Task<User> GetUserInfo(string email);
    }
}
