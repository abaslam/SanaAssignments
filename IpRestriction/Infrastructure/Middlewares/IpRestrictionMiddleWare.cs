namespace IpRestriction.Infrastructure.Middlewares
{
    using IpRestriction.Infrastructure.Configuration;
    using Microsoft.Extensions.Options;

    public class IpRestrictionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly IOptionsMonitor<AppSettings> optionsMonitor;

        public IpRestrictionMiddleWare(RequestDelegate next, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            this.next = next;
            this.optionsMonitor = optionsMonitor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress;
            var remoteIpString = remoteIp?.ToString();
            var restrictedIps = this.optionsMonitor.CurrentValue.RestrictedIPs;

            if (!restrictedIps.Contains(remoteIpString))
            {
                await this.next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: IP address is not allowed.");
            }
        }
    }
}
