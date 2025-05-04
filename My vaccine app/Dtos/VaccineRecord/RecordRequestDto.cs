using My_vaccine_app.Models;
using System.Text.Json.Serialization;

namespace My_vaccine_app.Dtos.VaccineRecord
{
    public class RecordRequestDto
    {
        public int UserId { get; set; }
        public int VaccineId { get; set; }
        public string AdministeredLocation { get; set; }
        public string AdministeredBy { get; set; }
        public int? DependentId { get; set; }
    }
}
