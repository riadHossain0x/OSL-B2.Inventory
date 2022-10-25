using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OSL_B2.Inventory.Membership.DbContexts;
using Unity;
using Unity.Injection;
using System.Web;

namespace OSL_B2.Inventory.Membership
{
    public static class MembershipModule
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<IUserStore<ApplicationUser, long>, UserStore>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IAccountAdapter, AccountAdapter>();
        }
    }
}
