namespace Events.ItAcademy.API.Infrastructure.Auth.JWT
{
    public class JWTConfiguration
    {
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
