namespace ConfigurableUI.Api.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<UserValue> Values { get; set; } = [];

    }
}
