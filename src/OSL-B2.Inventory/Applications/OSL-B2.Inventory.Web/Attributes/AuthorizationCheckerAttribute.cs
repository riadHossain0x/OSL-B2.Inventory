using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OSL_B2.Inventory.Web.Attributes
{
    public class AuthorizationCheckerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            Action = "Index",
                            Controller = "Home",
                            Area = "Admin"
                        }));
            }

            base.OnActionExecuting(context);
        }
    }
}