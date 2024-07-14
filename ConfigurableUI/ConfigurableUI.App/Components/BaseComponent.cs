namespace ConfigurableUI.App.Components
{
    using ConfigurableUI.App.Api.Entities;
    using Microsoft.AspNetCore.Components;

    public class BaseComponent : ComponentBase
    {
        [Parameter]
        public FieldValueDTO FieldValue { get; set; }
        [Parameter]
        public FieldDTO Field { get; set; }
    }
}
