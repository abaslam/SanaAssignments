using System.ComponentModel.DataAnnotations;

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
        [Display(Description = "Text box")]
        TextInput,
        [Display(Description = "Date input")]
        DateInput,
        [Display(Description = "Numeric input")]
        NumericInput,
        [Display(Description = "Read-only text")]
        TextDisplay,
        [Display(Description = "Read-only date")]
        DateDisplay,
        [Display(Description = "Read-only number")]
        NumberDisplay,
    }
}
