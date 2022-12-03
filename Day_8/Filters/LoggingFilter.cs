using Day_8.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ntq.Training.App.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Console.WriteLine($"(Logging Filter)Action Executing: {filterContext.ActionDescriptor.DisplayName}");
            var s = filterContext.HttpContext.Session.GetString(Session.ID);


            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                Console.WriteLine("(Logging Filter)Exception thrown");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
