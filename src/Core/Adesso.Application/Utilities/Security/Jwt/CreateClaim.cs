using Adesso.Domain.Models;
using System.Security.Claims;


namespace Adesso.Application.Utilities.Security.Jwt;

public static class CreateClaimHelper
{
    public static Claim[] CreateClaim(User user, List<string> roleNames)
    {
        var roleIds = new List<int>();
        List<string> roles = new List<string>();
        foreach (var role in Enum.GetValues(typeof(Domain.Enums.Roles)))
        {
            roles.Add(role.ToString());
        }




        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.Role, String.Join(", ", roleNames.ToArray())),
        };


        return claims;
    }
}
