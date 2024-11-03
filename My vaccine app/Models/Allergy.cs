using System.Text.Json.Serialization;

namespace My_vaccine_app.Models
{
    public class Allergy: TableBase
    {
        public int AllergyId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
