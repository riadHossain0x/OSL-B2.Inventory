using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OSL_B2.Inventory.Membership.DbContexts;
using Unity;
using System.Web;
using System.Data.Entity;
using OSL_B2.Inventory.Repository;

namespace OSL_B2.Inventory.Membership
{
    public static class MembershipModule
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<IUserStore<ApplicationUser, long>, UserStore>();
            container.RegisterFactory<IAuthenticationManager>(o => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterType<IAccountAdapter, AccountAdapter>();
            RepositoryModule.Register(container);
        }
    }
}
