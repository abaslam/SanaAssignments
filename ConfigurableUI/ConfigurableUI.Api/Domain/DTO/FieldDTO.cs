namespace ConfigurableUI.Api.Domain.DTO
{
    using ConfigurableUI.Api.Domain.Entities;

    public record FieldDTO(Guid Id, string Title, FieldType Type, FieldValueDTO? DefaultValue);
}
