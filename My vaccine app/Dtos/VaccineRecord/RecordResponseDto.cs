

using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.Response;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Models;

namespace My_vaccine_app.Dtos.VaccineRecord
{
    public class RecordResponseDto
    {
        public int VaccineRecordId { get; set; }
        public UserDto User { get; set; }
        public DependentDto Dependent { get; set; }
        public VaccineResponseDto Vaccine { get; set; }
        public DateTime DateAdministered { get; set; }
        public string AdministeredLocation { get; set; }
        public string AdministeredBy { get; set; }
    }
}
