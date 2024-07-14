namespace ConfigurableUI.Api.Features.Admin.Templates
{
    using ConfigurableUI.Api.Domain.DTO;
    using ConfigurableUI.Api.Domain.Entities;
    using ConfigurableUI.Api.Infrastructure.Persistence;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class Query
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/admin/template", (ApiDbContext apiDbContext) =>
            {
                var fields = apiDbContext.Fields
                                         .Select(x => new FieldDTO(x.Id, x.Title, x.Type, x.DefaultValue != null ? new FieldValueDTO(x.DefaultValue.Id, x.DefaultValue.NumericValue, x.DefaultValue.TextValue, x.DefaultValue.DateValue) : null))
                                         .ToList();
                var fieldTypes = new Dictionary<FieldType, string>();
                fieldTypes.Add(FieldType.TextInput, "Text box");
                fieldTypes.Add(FieldType.DateInput, "Date input");
                fieldTypes.Add(FieldType.NumericInput, "Numeric input");
                fieldTypes.Add(FieldType.TextDisplay, "Read-only text");
                fieldTypes.Add(FieldType.DateDisplay, "Read-only date");
                fieldTypes.Add(FieldType.NumberDisplay, "Read-only number");

                return Results.Ok(new GetTemplateResponse(fields, fieldTypes));
            });
        }

        public record GetTemplateResponse(List<FieldDTO> Fields, Dictionary<FieldType, string> FieldTypes);
    }
}
