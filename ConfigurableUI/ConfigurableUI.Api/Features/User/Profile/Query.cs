namespace ConfigurableUI.Api.Features.User.Profile
{
    using ConfigurableUI.Api.Domain.DTO;
    using ConfigurableUI.Api.Features.User.Profile.DTO;
    using ConfigurableUI.Api.Infrastructure.Persistence;

    public class Query
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/user/profile", (ApiDbContext dbContext, HttpContext httpContext) =>
            {
                var username = httpContext.User.Identity.Name;

                var fields = dbContext.Fields
                                         .Select(x => new FieldDTO(x.Id, x.Title, x.Type, x.DefaultValue != null ? new FieldValueDTO(x.DefaultValue.Id, x.DefaultValue.NumericValue, x.DefaultValue.TextValue, x.DefaultValue.DateValue) : null))
                                         .ToList();

                var fieldValues = dbContext.UserValues
                                           .Where(x => x.User.Name == username)
                                           .Select(x => new UserValueDTO(x.FieldId, new FieldValueDTO(x.Id, x.Value.NumericValue, x.Value.TextValue, x.Value.DateValue)))
                                           .ToList();
                return Results.Ok(new GetUserProfileResponse(fields, fieldValues));
            }).RequireAuthorization();
        }

        public record GetUserProfileResponse(List<FieldDTO> Fields, List<UserValueDTO> Values);
    }
}
