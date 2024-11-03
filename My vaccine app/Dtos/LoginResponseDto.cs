namespace My_vaccine_app.Dtos
{
    public class LoginResponseDto
    {
        public string token { get; set; }
        public DateTime expiration {  get; set; }
    }
}
