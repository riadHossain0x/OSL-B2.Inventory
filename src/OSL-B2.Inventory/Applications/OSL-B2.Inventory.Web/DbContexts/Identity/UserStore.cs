using Microsoft.AspNet.Identity.EntityFramework;

namespace OSL_B2.Inventory.Web.DbContexts
{
    public class UserStore : UserStore<ApplicationUser, Role, long,
    UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
