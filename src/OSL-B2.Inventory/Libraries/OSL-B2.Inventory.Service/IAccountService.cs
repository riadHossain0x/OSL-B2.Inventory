using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout);
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<bool> IsEmailConfirmedAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
    }
}
