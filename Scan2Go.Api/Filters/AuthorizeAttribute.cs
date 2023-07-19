using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scan2Go.Mapper.Models.UserModels;

namespace Scan2Go.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (UsersModel)context.HttpContext.Items["User"];
        bool anonymous = context.ActionDescriptor.EndpointMetadata.Any(t => t.GetType() == typeof(AllowAnonymousAttribute));
        if (user == null && !anonymous)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}