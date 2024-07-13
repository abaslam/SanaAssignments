using IpRestriction.Infrastructure.Configuration;
using IpRestriction.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.useIpRestriction();

app.MapGet("/", () => "Hello, World!")
    .WithName("HelloWorld")
    .Produces<string>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status403Forbidden);

app.Run();

