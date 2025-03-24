using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class ManagerRoleFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        // Block access for employees
        if (user.IsInRole("Employee"))
        {
            context.Result = new RedirectToActionResult("Login", "User", null);
            return;
        }

        // Allow only Managers and Admins
        if (!user.IsInRole("Manager") && !user.IsInRole("Admin"))
        {
            context.Result = new RedirectToActionResult("Login", "User", null);
        }
    }
}
