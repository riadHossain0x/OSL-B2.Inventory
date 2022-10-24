using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Membership.DbContexts;
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
            _accountAdapter.CreateAsync(new ApplicationUser { Email = "armana@gmail.com", UserName= "armaan@gmail.com" }, "Arman.00");
            return View();
        }
    }
}