namespace ConfigurableUI.Api.Infrastructure.Persistence.Configurations
{
    using ConfigurableUI.Api.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserValueConfiguration : IEntityTypeConfiguration<UserValue>
    {
        public void Configure(EntityTypeBuilder<UserValue> builder)
        {
            builder.HasOne(x => x.Field)
                .WithMany()
                .HasForeignKey(x => x.FieldId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x=>x.Value)
                .WithMany()
                .HasForeignKey(x => x.ValueId);
        }
    }
}
