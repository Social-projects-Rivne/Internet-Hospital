namespace InternetHospital.BusinessLogic.Models
{
    public class AppSettings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireMinutes { get; set; }
    }
}
