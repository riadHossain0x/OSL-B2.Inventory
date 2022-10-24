using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Repository.DbContexts;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Net;

namespace OSL_B2.Inventory.Membership
{
    public class AccountAdapter : IAccountAdapter
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountAdapter(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(long userId, string token)
        {
            return await _userManager.ConfirmEmailAsync(userId, token);
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if(!result.Succeeded)
                throw new InvalidOperationException("Failed to create user account!");

            // user service will be here.
            await SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return true;
        }

        public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            return await _userManager.FindByNameAsync(email);
        }

        public async Task<bool> IsEmailConfirmedAsync(long userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId);
        }

        public void LogOff()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        }

        public async Task<IdentityResult> ResetPasswordAsync(long userId, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            await _signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }
    }
}
