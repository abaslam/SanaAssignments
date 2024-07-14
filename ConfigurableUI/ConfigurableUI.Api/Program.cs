using ConfigurableUI.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
