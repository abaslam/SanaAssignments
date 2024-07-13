namespace IpRestriction.Infrastructure.Configuration
{
    public class AppSettings
    {
        public IEnumerable<string> RestrictedIPs { get; set; } = new List<string>();
    }
}
