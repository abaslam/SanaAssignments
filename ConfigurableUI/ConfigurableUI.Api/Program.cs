using ConfigurableUI.Api.Infrastructure.Configuration;
using ConfigurableUI.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
var appSettings = new AppSettings();
appSettingsSection.Bind(appSettings);

builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddAuthorization().AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = appSettings.JWTSettings.ValidIssuer,
        ValidAudience = appSettings.JWTSettings.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWTSettings.SecurityKey)),
    };
});

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
app.UseAuthentication();
app.UseAuthorization();

ConfigurableUI.Api.Features.Admin.Templates.Command.AddRoutes(app);
ConfigurableUI.Api.Features.Admin.Templates.Query.AddRoutes(app);

ConfigurableUI.Api.Features.Auth.Login.Command.AddRoutes(app);

ConfigurableUI.Api.Features.User.Profile.Command.AddRoutes(app);
ConfigurableUI.Api.Features.User.Profile.Query.AddRoutes(app);


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiDbContext>();
    await context.Initialize();
}

app.Run();
