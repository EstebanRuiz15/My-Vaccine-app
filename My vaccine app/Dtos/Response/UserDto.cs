using My_vaccine_app.Models;
using System.Text.Json.Serialization;

namespace My_vaccine_app.Dtos.Response
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Allergy> Allergies { get; set; }

    }
}
