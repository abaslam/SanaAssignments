namespace ConfigurableUI.Api.Features.User.Profile
{
    using ConfigurableUI.Api.Domain.Entities;
    using ConfigurableUI.Api.Features.User.Profile.DTO;
    using ConfigurableUI.Api.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    public class Command
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/user/profile", async (SaveUserProfileRequest request, ApiDbContext dbContext) =>
            {
                var defaultUser = dbContext.Users.FirstOrDefault(x => x.Name == "Default User");
                foreach (var value in request.Values)
                {
                    var existingValue = dbContext.UserValues.Include(x => x.Value).FirstOrDefault(x => x.FieldId == value.FieldId && x.UserId == defaultUser.Id);

                    if (existingValue != null)
                    {
                        existingValue.Value.TextValue = value.Value.TextValue;
                        existingValue.Value.NumericValue = value.Value.NumericValue;
                        existingValue.Value.DateValue = value.Value.DateValue;

                        dbContext.UserValues.Update(existingValue);
                    }
                    else
                    {
                        var newValue = new UserValue
                        {
                            Id = Guid.NewGuid(),
                            FieldId = value.FieldId,
                            UserId = defaultUser.Id,
                            Value = new FieldValue
                            {
                                Id = Guid.NewGuid(),
                                TextValue = value.Value.TextValue,
                                NumericValue = value.Value.NumericValue,
                                DateValue = value.Value.DateValue,
                            }
                        };

                        dbContext.UserValues.Add(newValue);
                    }
                }

                await dbContext.SaveChangesAsync();

                return Results.Ok();
            });
        }

        public record SaveUserProfileRequest(List<UserValueDTO> Values);
    }
}
