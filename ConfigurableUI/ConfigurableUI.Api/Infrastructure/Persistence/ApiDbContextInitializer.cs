using ConfigurableUI.Api.Domain.Entities;

namespace ConfigurableUI.Api.Infrastructure.Persistence
{
    public static class ApiDbContextInitializer
    {
        public static async Task Initialize(this ApiDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            var fields = new Field[]
            {
                new Field{Id=  Guid.NewGuid(), Title = "Name", Type = FieldType.TextInput },
                new Field{Id=  Guid.NewGuid(), Title = "Date of birth", Type = FieldType.DateInput },
                new Field{Id=  Guid.NewGuid(), Title = "Years of Experience", Type = FieldType.NumericInput },
                new Field{Id=  Guid.NewGuid(), Title = "Tax Code", Type = FieldType.NumberDisplay, DefaultValue= new FieldValue{ Id= Guid.NewGuid(), NumericValue = 123456789 } },
            };


            foreach (var field in fields)
            {
                dbContext.Fields.Add(field);
            }

            dbContext.Users.Add(new User { Id = Guid.NewGuid(), Name = "Default User" });

            await dbContext.SaveChangesAsync();
        }
    }
}
