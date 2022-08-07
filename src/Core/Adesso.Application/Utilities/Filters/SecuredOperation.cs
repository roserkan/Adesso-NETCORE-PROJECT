using Adesso.Application.Constants;
using Adesso.Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Adesso.Application.Utilities.Filters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SecuredOperationAttribute : Attribute, IAuthorizationFilter
{
    private string[] _roles;

    public SecuredOperationAttribute(string roles)
    {
        _roles = roles.Split(',');
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string filterString = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/roles";
        // authorization
        var roles = context.HttpContext.User.Claims.Where(c => c.Type == filterString);
        foreach (var role in roles)
        {
            if (_roles.Contains(role.Value))
            {
                return;
            }
        }

        throw new Exception(Messages.AuthorizationDenied);


    }
}   