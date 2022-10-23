using Microsoft.AspNet.Identity;
using OSL_B2.Inventory.Membership.Services;
using OSL_B2.Inventory.Repository.DbContexts;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Membership.Adapters
{
    public class AccountAdapter : IAccountAdapter
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public AccountAdapter(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
    }
}
