using Adesso.Domain.Models;
using System.Security.Claims;


namespace Adesso.Application.Utilities.Security.Jwt;

public static class CreateClaimHelper
{
    public static Claim[] CreateClaim(User user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.EmailAddress),
        };

        return claims;
    }
}
