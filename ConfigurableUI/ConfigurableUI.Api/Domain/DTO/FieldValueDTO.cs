namespace ConfigurableUI.Api.Domain.DTO
{
    public record FieldValueDTO(Guid Id, int? NumericValue, string? TextValue, DateTime? DateValue);
}
