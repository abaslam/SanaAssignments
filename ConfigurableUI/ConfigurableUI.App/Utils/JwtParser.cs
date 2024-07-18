namespace ConfigurableUI.App.Utils
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text.Json;

    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = jwtTokenHandler.ReadJwtToken(jwt);

            return jwtToken.Claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
