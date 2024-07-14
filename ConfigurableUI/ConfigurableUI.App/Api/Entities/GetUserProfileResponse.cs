namespace ConfigurableUI.App.Api.Entities
{
    public record GetUserProfileResponse(List<FieldDTO> Fields, List<UserValueDTO> Values);
}
