using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Membership.Adapters
{
    public interface IAccountAdapter
    {
        Task<IdentityResult> CreateUserAsync(object user);
        Task<object> GetUserByEmailAsync(string email);
        Task<object> GetUserAsync();
        Task<SignInStatus> PasswordSignInAsync(object user);
        Task<IList<string>> GetCurrentUserRolesAsync(string email);
        Task SignInAsync(string email);
        Task SignOutAsync();
        bool IsAuthenticated();
        string GetUserId();
    }
}
