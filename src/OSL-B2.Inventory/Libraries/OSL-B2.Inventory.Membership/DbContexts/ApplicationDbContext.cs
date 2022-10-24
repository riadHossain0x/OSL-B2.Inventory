using Microsoft.AspNet.Identity.EntityFramework;
using OSL_B2.Inventory.Entities.Entities;
using System.Data.Entity;

namespace OSL_B2.Inventory.Membership.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role,
    long, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
