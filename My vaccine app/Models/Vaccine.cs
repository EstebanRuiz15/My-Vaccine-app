using System.Text.Json.Serialization;

namespace My_vaccine_app.Models
{
    public class Vaccine: TableBase
    {
        public int VaccineId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<VaccineCategory> Categories { get; set; }
        public bool RequiresBooster { get; set; }
    }
}
