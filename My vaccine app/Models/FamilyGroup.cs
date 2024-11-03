using System.Text.Json.Serialization;

namespace My_vaccine_app.Models
{
    public class FamilyGroup: TableBase
    {
        public int FamilyGroupId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
