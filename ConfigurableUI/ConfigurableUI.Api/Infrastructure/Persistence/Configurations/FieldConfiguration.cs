namespace ConfigurableUI.Api.Infrastructure.Persistence.Configurations
{
    using ConfigurableUI.Api.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasOne(x => x.DefaultValue)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(x => x.DefaultValueId);
        }
    }
}
