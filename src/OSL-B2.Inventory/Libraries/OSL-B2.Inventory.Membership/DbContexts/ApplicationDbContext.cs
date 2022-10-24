using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OSL_B2.Inventory.Entities.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Membership.DbContexts
{
    public class ApplicationUser : IdentityUser<long, UserLogin, UserRole,
    UserClaim>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
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
    public class CustomUserStore : UserStore<ApplicationUser, Role, long,
    UserLogin, UserRole, UserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class UserRole : IdentityUserRole<long> { }
    public class UserClaim : IdentityUserClaim<long> { }
    public class UserLogin : IdentityUserLogin<long> { }

    public class Role : IdentityRole<long, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class CustomRoleStore : RoleStore<Role, long, UserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
