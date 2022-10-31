using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Web.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Adapters
{
    public interface IAccountAdapter
    {
        Task<bool> CreateAsync(ApplicationUser user, string password);
        Task<bool> CreateAsync(ApplicationUser user, ExternalLoginInfo info);
        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout);
        Task<ApplicationUser> FindByNameAsync(string email);
        ApplicationUser FindByName(string email);
        Task<ApplicationUser> FindByIdAsync(long id);
        ApplicationUser FindById(long id);
        Task<long> GetUserIdAsync();
        Task<bool> IsEmailConfirmedAsync(long userId);
        Task<IdentityResult> ConfirmEmailAsync(long userId, string token);
        Task<IdentityResult> ResetPasswordAsync(long userId, string token, string newPassword);
        void LogOff();

        Task<List<SelectListItem>> GetValidTwoFactorProvidersAsync(long userId);
    }
}
