namespace My_vaccine_app.Dtos.FamilyGroup
{
    public class GroupResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> users { get; set; }
    }
}
