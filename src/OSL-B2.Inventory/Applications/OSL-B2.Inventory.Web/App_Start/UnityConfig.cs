using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Membership.DbContexts;
using System.Data.Entity;
using Microsoft.Owin.Security;
using System.Web;
using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Repository;
using System;

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