using AutoMapper;
using My_vaccine_app.Dtos.FamilyGroup;
using My_vaccine_app.Models;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Configurations.Resolvers
{
    public class UserToUsersName : IValueResolver<FamilyGroup, GroupResponseDto, List<string>>
    {
        private readonly IUserService _userService;
        public UserToUsersName(IUserService userService)
        {
            _userService = userService;
        }

        public List<string> Resolve(FamilyGroup source, GroupResponseDto destination, List<string> destMember, ResolutionContext context)
        {
            var userIds = source.Users.Select(u => u.UserId).ToList();
            var userNames = new List<string>();

            foreach (var userId in userIds)
            {
                var user = _userService.GetById(userId).Result;
                if (user != null)
                {
                    userNames.Add(user.FirstName+" "+user.LastName); 
                }
            }

            return userNames;
        }
    }
}
