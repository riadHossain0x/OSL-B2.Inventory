using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Repository.DbContexts;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using OSL_B2.Inventory.Membership;

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
            //container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<IUserStore<ApplicationUser, long>, CustomUserStore>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IAccountAdapter, AccountAdapter>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}