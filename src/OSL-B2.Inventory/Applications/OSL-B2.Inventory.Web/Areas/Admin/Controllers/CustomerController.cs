using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController<CustomerController>
    {
        private readonly IAccountAdapter _accountAdapter;
        private readonly ICustomerService _customerService;

        public CustomerController(IAccountAdapter accountAdapter, ICustomerService customerService)
        {
            _accountAdapter = accountAdapter;
            _customerService = customerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);

                    var customer = model.GetCustomer(user.Id);

                    _customerService.AddCustomer(customer);

                    ViewResponse("Successfully added a new customer.", ResponseTypes.Success);

                    return RedirectToAction(nameof(Index), new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);

                    ViewResponse(ex.Message, ResponseTypes.Danger);
                }
            }
            return View(model);
        }
    }
}