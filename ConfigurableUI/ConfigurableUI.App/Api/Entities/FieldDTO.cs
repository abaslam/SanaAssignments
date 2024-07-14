namespace ConfigurableUI.App.Api.Entities
{
    using ConfigurableUI.App.Components;

    public record FieldDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public FieldType Type { get; set; }
        public FieldValueDTO? DefaultValue { get; set; }

        public Type GetDefaultValueComponent()
        {
            if (this.Type == FieldType.TextDisplay)
            {
                return typeof(TextInput);
            }

            if (this.Type == FieldType.DateDisplay)
            {
                return typeof(DateInput);
            }

            if (this.Type == FieldType.NumberDisplay)
            {
                return typeof(NumericInput);
            }

            return typeof(Display);
        }

        public Type GetComponentType()
        {
            if (this.Type == FieldType.TextInput)
            {
                return typeof(TextInput);
            }

            if (this.Type == FieldType.DateInput)
            {
                return typeof(DateInput);
            }

            if (this.Type == FieldType.NumericInput)
            {
                return typeof(NumericInput);
            }

            return typeof(Display);
        }

        public Dictionary<string, object?> GetParams()
        {
            return new Dictionary<string, object?>
            {
                ["FieldValue"] = this.DefaultValue,
            };
        }

        public Dictionary<string, object?> GetParams(List<UserValueDTO> values)
        {
            var value = values.FirstOrDefault(x => x.FieldId == this.Id)?.Value;

            if (value == null && !new[] { FieldType.DateDisplay, FieldType.TextDisplay, FieldType.NumberDisplay }.Contains(this.Type))
            {
                value = new FieldValueDTO { Id = Guid.NewGuid() };

                values.Add(new UserValueDTO(this.Id, value));
            }

            return new Dictionary<string, object?>
            {
                ["FieldValue"] = value ?? this.DefaultValue,
            };
        }
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
