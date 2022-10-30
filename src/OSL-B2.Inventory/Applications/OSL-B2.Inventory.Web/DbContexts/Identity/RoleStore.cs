using Microsoft.AspNet.Identity.EntityFramework;

namespace OSL_B2.Inventory.Web.DbContexts
{
    public class RoleStore : RoleStore<Role, long, UserRole>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
