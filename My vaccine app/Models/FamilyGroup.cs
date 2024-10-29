namespace My_vaccine_app.Models
{
    public class FamilyGroup
    {
        public int FamilyGroupId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
