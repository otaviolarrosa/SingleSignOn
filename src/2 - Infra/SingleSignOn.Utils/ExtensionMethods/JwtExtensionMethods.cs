using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace SingleSignOn.Utils.ExtensionMethods
{
    public static class JwtExtensionMethods
    {
        public static IPrincipal GetPrincipalFromJwtToken(this string token, out SecurityToken securityToken)
        {
            var symmetricKey = Convert.FromBase64String(AppSettings.SecretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            return principal;
        }

        public static bool IsValidJwtToken(this string token)
        {
            _ = token.GetPrincipalFromJwtToken(out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return true;
        }
    }
}
