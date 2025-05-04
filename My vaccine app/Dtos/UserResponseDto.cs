namespace My_vaccine_app.Dtos
{
    public class UserResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public bool IsSucces { get; set; } 
    }
}
