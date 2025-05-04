using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Implementations;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _userRepository.FindBy(x => x.UserId == id)
                .Include(u => u.Dependents)
                .Include(u => u.FamilyGroups)
                .Include(u => u.VaccineRecords)
                .Include(u => u.Allergies)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                await _userRepository.Delete(user);
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll()
                .Include(u => u.Dependents)
                .Include(u => u.FamilyGroups)
                .Include(u => u.VaccineRecords)
                .Include(u => u.Allergies)
                .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.FindBy(x => x.UserId == id)
                .Include(u => u.Dependents)
                .Include(u => u.FamilyGroups)
                .Include(u => u.VaccineRecords)
                .Include(u => u.Allergies)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserInfo(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null) return null;

            var response = await _userRepository.FindByAsNoTracking(x => x.AspNetUserId == user.Id)
                .Include(u => u.Dependents)
                .Include(u => u.FamilyGroups)
                .Include(u => u.VaccineRecords)
                .Include(u => u.Allergies)
                .FirstOrDefaultAsync();

            return response;
        }
    }
}
