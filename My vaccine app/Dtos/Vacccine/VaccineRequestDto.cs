namespace My_vaccine_app.Dtos.Vacccine
{
    public class VaccineRequestDto
    {
        public string Name { get; set; }
        public bool RequieredBooster { get; set; }
        public string[] Categories { get; set; }
    }
}
