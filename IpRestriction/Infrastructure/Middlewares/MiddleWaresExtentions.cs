namespace IpRestriction.Infrastructure.Middlewares
{
    public static class MiddlewaresExtentions
    {
        public static IApplicationBuilder UseIpRestriction(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpRestrictionMiddleWare>();
        }
    }
}
