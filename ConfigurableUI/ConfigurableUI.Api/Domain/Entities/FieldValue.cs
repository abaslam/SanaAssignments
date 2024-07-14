namespace ConfigurableUI.Api.Domain.Entities
{
    public class FieldValue
    {
        public Guid Id { get; set; }
        public int? NumericValue { get; set; }
        public string? TextValue { get; set; }
        public DateTime? DateValue { get; set; }
    }
}
