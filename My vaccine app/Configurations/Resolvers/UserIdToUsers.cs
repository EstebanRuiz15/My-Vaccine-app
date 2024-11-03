using AutoMapper;
using My_vaccine_app.Dtos.FamilyGroup;
using My_vaccine_app.Models;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Configurations.Resolvers
{
    public class UserIdToUsers :IValueResolver<GroupRequestDto, FamilyGroup, List<User>>
    {
        private readonly IUserService _userService;

        public UserIdToUsers(IUserService userService)
        {
            _userService = userService;
        }

        public List<User> Resolve(GroupRequestDto source, FamilyGroup destination, List<User> destMember, ResolutionContext context)
        {
            var users = new List<User>();

            foreach (var userId in source.Users)
            {
                var user = _userService.GetById(userId).Result;
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }
    }
}
