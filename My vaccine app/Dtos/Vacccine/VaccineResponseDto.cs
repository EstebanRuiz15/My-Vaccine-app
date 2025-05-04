using My_vaccine_app.Dtos.Categories;
using My_vaccine_app.Models;

namespace My_vaccine_app.Dtos.Vacccine
{
    public class VaccineResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool RequieredBooster { get; set; }
        public List<CategoriResponseDto> Categories { get; set; }
    }
}
