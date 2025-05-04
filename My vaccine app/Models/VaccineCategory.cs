using System.Text.Json.Serialization;

namespace My_vaccine_app.Models
{
    public class VaccineCategory: TableBase
    {
        public int VaccineCategoryId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Vaccine> Vaccines { get; set; }
    }
}
