using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Neosoft_LeaveManagement.Filters
{
    public class RequireLoginFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userId = session.GetInt32("UserId");

            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed after execution
        }
    }
}
