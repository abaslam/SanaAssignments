namespace ConfigurableUI.Api.Domain.Entities
{
    public class Field
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public FieldType Type { get; set; }
        public Guid? DefaultValueId { get; set; }
        public virtual FieldValue? DefaultValue { get; set; }
    }

    public enum FieldType
    {
        TextInput,     
        DateInput,     
        NumericInput,
        TextDisplay,        
        DateDisplay,        
        NumberDisplay,
    }
}
