using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Membership.Adapters
{
    public interface IAccountAdapter
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        //Task<SignInStatus> PasswordSignInAsync(string email, string password, bool isPersistant, bool shouldLockout);
    }
}
