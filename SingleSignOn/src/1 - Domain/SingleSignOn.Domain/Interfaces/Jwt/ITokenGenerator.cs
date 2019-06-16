using System.Security.Claims;

namespace SingleSignOn.Domain.Interfaces.Jwt
{
    public interface ITokenGenerator
    {
        string GenerateToken(string username);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
