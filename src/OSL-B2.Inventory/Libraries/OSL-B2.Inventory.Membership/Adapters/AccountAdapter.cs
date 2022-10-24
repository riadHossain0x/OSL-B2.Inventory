using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Membership.DbContexts;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

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
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return true;
            }

            throw new InvalidOperationException($"{string.Join(", ", result.Errors)}");
        }

        public async Task<bool> CreateAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user.Id, info.Login);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return true;
                }
            }

            throw new InvalidOperationException($"{string.Join(", ", result.Errors)}");
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent);
        }

        public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            return await _userManager.FindByNameAsync(email);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _authenticationManager.GetExternalLoginInfoAsync();
        }

        public async Task<long> GetUserIdAsync()
        {
            return await _signInManager.GetVerifiedUserIdAsync();
        }

        public async Task<List<SelectListItem>> GetValidTwoFactorProvidersAsync(long userId)
        {
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            return userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
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
