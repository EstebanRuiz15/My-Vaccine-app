using System.Text.Json.Serialization;

namespace My_vaccine_app.Models
{
    public class Dependent: TableBase
    {
        public int DependentId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public List<VaccineRecord> VaccineRecords { get; set; }
    }
}
