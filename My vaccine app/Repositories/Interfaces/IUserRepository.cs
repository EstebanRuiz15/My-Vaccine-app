using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_vaccine_app.Dtos;
using My_vaccine_app.Models;

namespace My_vaccine_app.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IdentityResult> addUser(RequestRegisterDto request);
        Task<LoginResponseDto> login(LoginRequestDto request);
        Task<LoginResponseDto> refresh(string email);
    }
}
