namespace My_vaccine_app.Literals
{
    public static class MyVaccineLiterals
    {
        public const string MY_VACCINE_CONECTION_APP = "MY_VACCINE_CONECTION";
        public static string JWT_KEY
        {
            get
            {
                var key = Environment.GetEnvironmentVariable("JWT_KEY");
                if (string.IsNullOrEmpty(key))
                    throw new InvalidOperationException("JWT_KEY environment variable is not set");
                return key;
            }
        }
    }
}