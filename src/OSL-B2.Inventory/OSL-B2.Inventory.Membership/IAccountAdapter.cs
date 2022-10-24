using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Membership
{
    public interface IAccountAdapter
    {
        Task<bool> CreateAsync(ApplicationUser user, string password);
        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout);
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<bool> IsEmailConfirmedAsync(long userId);
        Task<IdentityResult> ConfirmEmailAsync(long userId, string token);
        Task<IdentityResult> ResetPasswordAsync(long userId, string token, string newPassword);
        void LogOff();
    }
}
