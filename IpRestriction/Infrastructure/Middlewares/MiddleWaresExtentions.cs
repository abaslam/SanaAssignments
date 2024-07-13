namespace IpRestriction.Infrastructure.Middlewares
{
    public static class MiddleWaresExtentions
    {
        public static IApplicationBuilder useIpRestriction(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpRestrictionMiddleWare>();
        }
    }
}
