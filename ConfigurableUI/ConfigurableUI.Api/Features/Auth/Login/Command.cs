namespace ConfigurableUI.Api.Features.Auth.Login
{
    using ConfigurableUI.Api.Infrastructure.Configuration;
    using ConfigurableUI.Api.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class Command
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/auth/login", async (ApiDbContext dbContext, LoginRequest request, IOptions<AppSettings> appSettingOptions) =>
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Name == request.Username);

                if (user == null || !string.Equals(user.Password, request.Password))
                {
                    return Results.Unauthorized();
                }

                var appSettings = appSettingOptions.Value;

                var key = Encoding.UTF8.GetBytes(appSettings.JWTSettings.SecurityKey);
                var secret = new SymmetricSecurityKey(key);

                var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                };

                var tokenOptions = new JwtSecurityToken(
                        issuer: appSettings.JWTSettings.ValidIssuer,
                        audience: appSettings.JWTSettings.ValidAudience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(appSettings.JWTSettings.ExpiryInMinutes),
                        signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Results.Ok(new LoginResponse(true, token));
            });
        }

        public record LoginRequest(string Username, string Password);
        public record LoginResponse(bool Success, string Token);
    }
}
