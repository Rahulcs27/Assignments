using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class ManagerRoleFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Session.GetInt32("UserRole");

        //if (!user.Identity.IsAuthenticated)
        //{
        //    context.Result = new RedirectToActionResult("Login", "User", null);
        //    return;
        //}

        // Block access for employees
        if (user != 2)
        {
            context.Result = new RedirectToActionResult("Login", "User", null);
            return;
        }
    }
}
