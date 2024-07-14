namespace ConfigurableUI.App.Api.Entities
{
    public record GetTemplateResponse(List<FieldDTO> Fields, Dictionary<FieldType, string> FieldTypes);
}
