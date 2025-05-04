using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Dtos.VaccineRecord;
using My_vaccine_app.Models;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Configurations.Resolvers.VaccineRecordd
{
    public class VaccineIdToVaccineResolver : IValueResolver<RecordRequestDto, VaccineRecord, Vaccine>
    {
        private readonly IVaccineService _vaccineService;
        private readonly IMapper _mapper;
        public VaccineIdToVaccineResolver(IVaccineService vaccineService, IMapper mapper)
        {
            _vaccineService = vaccineService;
            _mapper = mapper;
        }

        public Vaccine Resolve(RecordRequestDto source, VaccineRecord destination, Vaccine destMember, ResolutionContext context)
        {
            var vaccine = _vaccineService.GetById(source.VaccineId).Result;
            return _mapper.Map<Vaccine>(vaccine);
        }
    }
    
}
