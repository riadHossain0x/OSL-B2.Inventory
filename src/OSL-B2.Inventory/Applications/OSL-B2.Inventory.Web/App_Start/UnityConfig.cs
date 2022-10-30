using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using OSL_B2.Inventory.Web.Areas.Admin.Controllers;
using OSL_B2.Inventory.Service;

namespace OSL_B2.Inventory.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ManageController>(new InjectionConstructor());
            WebModule.Register(container);
            ServiceModule.Register(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}