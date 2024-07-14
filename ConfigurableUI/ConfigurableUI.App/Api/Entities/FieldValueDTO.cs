namespace ConfigurableUI.App.Api.Entities
{
    public record FieldValueDTO
    {
        public Guid Id { get; set; }
        public int? NumericValue { get; set; }
        public string TextValue { get; set; }
        public DateTime? DateValue { get; set; }

        public string DisplayValue { get => this.NumericValue.HasValue ? this.NumericValue.Value.ToString() : this.DateValue.HasValue ? this.DateValue.Value.ToShortDateString() : this.TextValue; }
    }
}
