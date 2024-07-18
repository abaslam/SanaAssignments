namespace ConfigurableUI.Api.Features.Admin.Templates
{
    using ConfigurableUI.Api.Domain.DTO;
    using ConfigurableUI.Api.Domain.Entities;
    using ConfigurableUI.Api.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    public class Command
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/admin/template", async (SaveTemplateRequest request, ApiDbContext dbContext) =>
            {
                foreach (var field in request.Fields)
                {
                    var existingField = dbContext.Fields.Include(x => x.DefaultValue).FirstOrDefault(x => x.Id == field.Id);

                    if (existingField != null)
                    {
                        existingField.Title = field.Title;
                        existingField.Type = field.Type;

                        if (new[] { FieldType.DateDisplay, FieldType.TextDisplay, FieldType.NumberDisplay }.Contains(field.Type))
                        {
                            if (existingField.DefaultValue == null)
                            {
                                existingField.DefaultValue = new FieldValue();
                            }

                            existingField.DefaultValue.TextValue = field.DefaultValue.TextValue;
                            existingField.DefaultValue.DateValue = field.DefaultValue.DateValue;
                            existingField.DefaultValue.NumericValue = field.DefaultValue.NumericValue;
                        }
                        else if (existingField.DefaultValue != null)
                        {
                            dbContext.FieldValues.Remove(existingField.DefaultValue);
                            existingField.DefaultValue = null;
                            existingField.DefaultValueId = null;
                        }

                        dbContext.Fields.Update(existingField);
                    }
                    else
                    {
                        var newField = new Field
                        {
                            Id = field.Id,
                            Title = field.Title,
                            Type = field.Type,
                        };

                        if (new[] { FieldType.DateDisplay, FieldType.TextDisplay, FieldType.NumberDisplay }.Contains(field.Type))
                        {
                            newField.DefaultValue = new FieldValue
                            {
                                TextValue = field.DefaultValue.TextValue,
                                DateValue = field.DefaultValue.DateValue,
                                NumericValue = field.DefaultValue.NumericValue
                            };
                        }

                        dbContext.Fields.Add(newField);
                    }
                }

                await dbContext.SaveChangesAsync();

                return Results.Created();
            }).RequireAuthorization();
        }

        public record SaveTemplateRequest(List<FieldDTO> Fields);
    }
}
