using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace QLBanCay.Models.Authentication
{
    public class Authentication : ActionFilterAttribute    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller", "Access" },
                    {"Action", "Login" }
            });
        }
    }

}

