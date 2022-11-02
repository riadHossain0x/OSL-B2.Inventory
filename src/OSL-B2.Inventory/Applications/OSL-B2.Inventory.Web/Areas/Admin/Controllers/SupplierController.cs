using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class SupplierController : AdminBaseController<SupplierController>
    {
        #region Initialization
        private readonly ISupplierService _supplierService;
        private readonly IAccountAdapter _accountAdapter;

        public SupplierController(ISupplierService supplierService, IAccountAdapter accountAdapter)
        {
            _supplierService = supplierService;
            _accountAdapter = accountAdapter;
        } 
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region Operations
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SupplierCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);

                    var supplier = model.GetSupplier(user.Id);

                    _supplierService.AddSupplier(supplier);

                    ViewResponse("Successfully added a new supplier.", ResponseTypes.Success);

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