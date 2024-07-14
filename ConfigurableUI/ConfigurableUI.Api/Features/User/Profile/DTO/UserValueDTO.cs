namespace ConfigurableUI.Api.Features.User.Profile.DTO
{
    using ConfigurableUI.Api.Domain.DTO;

    public record UserValueDTO(Guid FieldId, FieldValueDTO Value);
}
