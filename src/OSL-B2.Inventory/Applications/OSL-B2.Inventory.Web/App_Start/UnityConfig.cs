using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Membership.DbContexts;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Repository.DbContexts;

namespace OSL_B2.Inventory.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<IUserStore<ApplicationUser, long>, UserStore>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IAccountAdapter, AccountAdapter>();
            container.RegisterType<IIMSDbContext, IMSDbContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}