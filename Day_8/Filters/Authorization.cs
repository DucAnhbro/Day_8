using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ntq.Training.App.Filters
{
    public class Authorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Account" },
            //                              { "action", "Login" }});
            base.OnActionExecuting(filterContext);
        }
    }
}

