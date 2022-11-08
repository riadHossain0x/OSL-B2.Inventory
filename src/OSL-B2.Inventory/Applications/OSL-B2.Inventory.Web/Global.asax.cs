using AutoMapper;
using OSL_B2.Inventory.Service.Profiles;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WholeSale.Web.Profiles;

namespace OSL_B2.Inventory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            Mapper.Initialize(x => { x.AddProfile<WebProfile>(); x.AddProfile<ServiceProfile>(); });
        }

        protected void Application_Error()
        {
            Server.ClearError();
            Response.Redirect("/Error/Index");
        }
    }
}
