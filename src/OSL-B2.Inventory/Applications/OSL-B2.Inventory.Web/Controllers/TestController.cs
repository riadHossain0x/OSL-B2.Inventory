using Microsoft.AspNet.Identity;
using OSL_B2.Inventory.Membership.Adapters;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WholeSale.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IAccountAdapter _accountAdapter;

        public TestController(IAccountAdapter accountAdapter)
        {
            _accountAdapter = accountAdapter;
        }
        // GET: Test
        public ActionResult Index()
        {
            _accountAdapter.CreateAsync(new ApplicationUser { Email = "arman@gmail.com", UserName= "arman@gmail.com" }, "Arman.00");
            return View();
        }
    }
}