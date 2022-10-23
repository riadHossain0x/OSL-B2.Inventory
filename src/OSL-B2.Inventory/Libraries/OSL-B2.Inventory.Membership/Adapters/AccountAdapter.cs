using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OSL_B2.Inventory.Membership.Services;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OSL_B2.Inventory.Membership.Adapters
{
    public class AccountAdapter : IAccountAdapter
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationSignInManager signInManager;

        //private readonly UserManager<ApplicationUser> _userManager;

        public AccountAdapter(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //_userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return null;
            //return await _userManager.CreateAsync(user, password);
        }
    }

    public class SignInManager : SignInManager<ApplicationUser, string>
    {
        public SignInManager(UserManager<ApplicationUser, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
}
