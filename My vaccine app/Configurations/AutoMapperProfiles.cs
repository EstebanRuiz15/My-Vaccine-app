using AutoMapper;
using My_vaccine_app.Configurations.Resolvers;
using My_vaccine_app.Configurations.Resolvers.VaccineRecordd;
using My_vaccine_app.Dtos;
using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Categories;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.FamilyGroup;
using My_vaccine_app.Dtos.Response;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Dtos.VaccineRecord;
using My_vaccine_app.Models;

namespace My_vaccine_app.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Dependent, Dtos.Dependent.DependentRequestDto>().ReverseMap();
            CreateMap<Dependent, DependentResponseDto>()
                .ForMember(dest => dest.Id, op => op.MapFrom(sour => sour.DependentId)).ReverseMap();

            CreateMap<Allergy, Dtos.Allergies.AllergyRequestDto>().ReverseMap();
            CreateMap<Allergy, AllergyResponseDto>()
                .ForMember(dest => dest.Id, op => op.MapFrom(sour => sour.AllergyId)).ReverseMap();

            CreateMap<VaccineRequestDto, Vaccine>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom<VaccineCategoryResolver>());

            CreateMap<VaccineRequestDto, Vaccine>()
                .ForMember(dest => dest.VaccineId, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore());
            CreateMap<Vaccine, VaccineResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VaccineId))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories)).ReverseMap();

            CreateMap<List<string>, List<VaccineCategory>>()
      .ConvertUsing<StringToCategoryListConverter>();

            CreateMap<List<VaccineCategory>, List<string>>()
                .ConvertUsing(src => src.Select(c => c.Name).ToList());

            CreateMap<VaccineCategory, CategoriRequestDto>().ReverseMap();
            CreateMap<VaccineCategory, CategoriResponseDto>()
                .ForMember(dest => dest.Id, op => op.MapFrom(source => source.VaccineCategoryId)).ReverseMap();

            CreateMap<FamilyGroup, GroupRequestDto>().ReverseMap()
            .ForMember(dest => dest.Users, opt => opt.MapFrom<UserIdToUsers>()).ReverseMap();
            CreateMap<GroupRequestDto, FamilyGroup>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom<UserIdToUsers>()).ReverseMap();
            CreateMap<FamilyGroup, GroupResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FamilyGroupId))
            .ForMember(dest => dest.users, opt => opt.MapFrom<UserToUsersName>());

            CreateMap<RecordRequestDto, VaccineRecord>()
            .ForMember(dest => dest.VaccineId, opt => opt.MapFrom(src => src.VaccineId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Vaccine, opt => opt.Ignore())
            .ForMember(dest => dest.Dependent, opt => opt.Ignore());
            CreateMap<VaccineRecord, RecordResponseDto>()
           .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
           .ForMember(dest => dest.Dependent, opt => opt.MapFrom(src => src.Dependent))
           .ForMember(dest => dest.Vaccine, opt => opt.MapFrom(src => src.Vaccine));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies)); 
            CreateMap<Dependent, DependentDto>();
            CreateMap<Vaccine, VaccineResponseDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        }
    }
}
