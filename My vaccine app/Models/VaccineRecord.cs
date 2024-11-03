using System.Text.Json.Serialization;

namespace My_vaccine_app.Models
{
    public class VaccineRecord: TableBase
    {
        public int VaccineRecordId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int? DependentId { get; set; }
        [JsonIgnore]
        public Dependent? Dependent { get; set; }
        public int VaccineId { get; set; }
        [JsonIgnore]
        public Vaccine Vaccine { get; set; }
        public DateTime DateAdministered { get; set; }
        public string AdministeredLocation { get; set; }
        public string AdministeredBy { get; set; }
    }
}
