using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Membership.Adapters;
using OSL_B2.Inventory.Repository.DbContexts;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System;
using OSL_B2.Inventory.Membership.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;

namespace OSL_B2.Inventory.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IAccountAdapter, AccountAdapter>();
            container.RegisterType<ITestService, TestService>();
            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}