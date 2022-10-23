using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Repository.DbContexts;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public AccountService(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            return await _userManager.ConfirmEmailAsync(userId, token);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            return await _userManager.FindByNameAsync(email);
        }

        public async Task<bool> IsEmailConfirmedAsync(string userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId);
        }

        public async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            await _signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }
    }
}
