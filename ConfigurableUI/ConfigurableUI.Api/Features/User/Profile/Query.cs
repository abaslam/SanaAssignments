using ConfigurableUI.Api.Domain.DTO;
using ConfigurableUI.Api.Features.User.Profile.DTO;
using ConfigurableUI.Api.Infrastructure.Persistence;

namespace ConfigurableUI.Api.Features.User.Profile
{
    public class Query
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/user/profile", (ApiDbContext dbContext) =>
            {

                var fields = dbContext.Fields
                                         .Select(x => new FieldDTO(x.Id, x.Title, x.Type, x.DefaultValue != null ? new FieldValueDTO(x.DefaultValue.Id, x.DefaultValue.NumericValue, x.DefaultValue.TextValue, x.DefaultValue.DateValue) : null))
                                         .ToList();

                var fieldValues = dbContext.UserValues
                                            .Select(x => new UserValueDTO(x.FieldId, new FieldValueDTO(x.Id, x.Value.NumericValue, x.Value.TextValue, x.Value.DateValue)))
                                            .ToList();
                return Results.Ok(new GetUserProfileResponse(fields, fieldValues));
            });
        }

        public record GetUserProfileResponse(List<FieldDTO> Fields, List<UserValueDTO> Values);
    }
}
