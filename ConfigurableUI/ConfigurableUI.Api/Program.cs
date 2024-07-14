using ConfigurableUI.Api.Features.Admin.Templates;
using ConfigurableUI.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiDbContext>(x => x.UseInMemoryDatabase("ConfigurableUI"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

ConfigurableUI.Api.Features.Admin.Templates.Command.AddRoutes(app);
ConfigurableUI.Api.Features.Admin.Templates.Query.AddRoutes(app);

ConfigurableUI.Api.Features.User.Profile.Command.AddRoutes(app);
ConfigurableUI.Api.Features.User.Profile.Query.AddRoutes(app);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiDbContext>();
    await context.Initialize();
}

app.Run();
