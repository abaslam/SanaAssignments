
namespace ConfigurableUI.Api.Infrastructure.Persistence
{
    using ConfigurableUI.Api.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<Field> Fields => Set<Field>();
        public DbSet<FieldValue> FieldValues => Set<FieldValue>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserValue> UserValues => Set<UserValue>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}
