using OSL_B2.Inventory.Web;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static OSL_B2.Inventory.Web.Models.DataTableAjaxPostModels;
using OSL_B2.Inventory.Web.Adapters;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController<CustomerController>
    {
        #region initialization
        private readonly IAccountAdapter _accountAdapter;
        private readonly ICustomerService _customerService;

        public CustomerController(IAccountAdapter accountAdapter, ICustomerService customerService)
        {
            _accountAdapter = accountAdapter;
            _customerService = customerService;
        } 
        #endregion

        #region Manage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCustomers(DataTableAjaxPostModel parameters)
        {

            string sortBy = "";
            bool sortDir = true;

            var value = (parameters.search != null) ? parameters.search.value : null;

            var data = _customerService.LoadAllCustomers(value, parameters.length, parameters.start, sortBy, sortDir);

            return Json(new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Email,
                                record.Mobile,
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Operations
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
        #endregion
    }
}