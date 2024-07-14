namespace ConfigurableUI.Api.Domain.Entities
{
    public class UserValue
    {
        public Guid Id { get; set; }
        public Guid FieldId { get; set; }
        public Guid UserId { get; set; }
        public Guid ValueId { get; set; }

        public virtual Field Field { get; set; }
        public virtual FieldValue Value { get; set; }
        public virtual User User { get; set; }
    }
}
