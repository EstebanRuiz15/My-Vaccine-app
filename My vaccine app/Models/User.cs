using System.Text.Json.Serialization;

namespace My_vaccine_app.Models;

    public class User: TableBase
{
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        [JsonIgnore]
        public string AspNetUserId { get; set; }
        [JsonIgnore]
        public AplicationUser AspNetUser { get; set; }
        public List<Dependent> Dependents { get; set; }
        public List<FamilyGroup> FamilyGroups { get; set; }
        public List<VaccineRecord> VaccineRecords { get; set; }
        public List<Allergy> Allergies { get; set; }
    }

