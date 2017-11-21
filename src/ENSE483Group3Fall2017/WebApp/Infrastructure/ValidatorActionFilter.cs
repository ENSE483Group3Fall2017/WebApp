using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Infrastructure
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
                if (filterContext.HttpContext.Request.Method == "GET")
                    filterContext.Result = new BadRequestResult();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
