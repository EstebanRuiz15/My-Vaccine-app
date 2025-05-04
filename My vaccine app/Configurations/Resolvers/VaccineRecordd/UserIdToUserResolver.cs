using AutoMapper;
using My_vaccine_app.Dtos.VaccineRecord;
using My_vaccine_app.Models;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Configurations.Resolvers.VaccineRecordd
{
    public class UserIdToUserResolver : IValueResolver<RecordRequestDto, VaccineRecord, User>
    {
        private readonly IUserService _userService;

        public UserIdToUserResolver(IUserService userService)
        {
            _userService = userService;
        }

        public User Resolve(RecordRequestDto source, VaccineRecord destination, User destMember, ResolutionContext context)
        {
            var user = _userService.GetById(source.UserId).Result;
            return user;
        }
    }
}
