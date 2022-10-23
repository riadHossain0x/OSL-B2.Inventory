using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OSL_B2.Inventory.Entities.Entities;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().Property(x => x.FirstName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<AppUser>().Property(x => x.LastName).HasColumnType("varchar").HasMaxLength(50);
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
