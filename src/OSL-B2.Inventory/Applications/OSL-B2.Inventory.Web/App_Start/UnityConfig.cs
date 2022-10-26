using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Web.Areas.Admin.Controllers;

namespace OSL_B2.Inventory.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ManageController>(new InjectionConstructor());
            MembershipModule.Register(container);
            RepositoryModule.Register(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}