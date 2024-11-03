
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos;
using My_vaccine_app.Literals;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace My_vaccine_app.Repositories.Implementations
{


    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        private readonly MyVaccineAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(MyVaccineAppDbContext context, UserManager<IdentityUser> userManager) : base(context)
        {
            _userManager = userManager;
            _context = context;

        }
        public async Task<IdentityResult> addUser(RequestRegisterDto request)
        {
            var response = new IdentityResult();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var user = new AplicationUser

                {
                    UserName = request.Email.ToLower(),
                    Email = request.Email
                };
                var result = await _userManager.CreateAsync(user, request.Password);

                response = result;
                if (!result.Succeeded)
                {

                    return response;
                }

                var newUser = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    AspNetUserId = user.Id

                };

                await _context.AddAsync(newUser);
                await _context.SaveChangesAsync();
                scope.Complete();
            }

            return IdentityResult.Success;
        }

        public async Task<LoginResponseDto> login(LoginRequestDto request)
        {
            var response = new LoginResponseDto();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(MyVaccineLiterals.JWT_KEY)));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: creds
                        );

                    response.token = new JwtSecurityTokenHandler().WriteToken(token);
                    response.expiration = token.ValidTo;

                    scope.Complete();
                    return response;
                }

            }
            return response;
        }

        public async Task<LoginResponseDto> refresh(string email)
        {
            var response = new LoginResponseDto();
            var user = await _userManager.FindByNameAsync(email);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(MyVaccineLiterals.JWT_KEY)));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: creds
                    );
                response.token = new JwtSecurityTokenHandler().WriteToken(token);
                response.expiration = token.ValidTo;
                return response;
            }

            return response;
        }

    }
}
