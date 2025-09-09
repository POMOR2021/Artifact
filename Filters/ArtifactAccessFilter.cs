using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class ArtifactAccessFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.ContainsKey("status") &&
            context.ActionArguments["status"]?.ToString() == "в хранилище")
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated || !user.IsInRole("Admin"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}
