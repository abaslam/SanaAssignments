namespace ConfigurableUI.Api.Infrastructure.Configuration
{
    public class AppSettings
    {
        public JWTSettings JWTSettings { get; set; } = new JWTSettings();
    }

    public class JWTSettings
    {
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpiryInMinutes { get; set; }

    }
}

