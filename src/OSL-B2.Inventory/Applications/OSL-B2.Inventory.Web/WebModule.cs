using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.DbContexts;
using System.Data.Entity;
using System.Web;
using Unity;

namespace OSL_B2.Inventory.Web
{
    public static class WebModule
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<IUserStore<ApplicationUser, long>, UserStore>();
            container.RegisterFactory<IAuthenticationManager>(o => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterType<IAccountAdapter, AccountAdapter>();
        }
    }
}