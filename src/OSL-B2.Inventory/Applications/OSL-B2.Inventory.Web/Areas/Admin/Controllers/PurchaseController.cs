using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class PurchaseController : AdminBaseController<PurchaseController>
    {
        #region Initialization
        private readonly IAccountAdapter _accountAdapter;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;

        public PurchaseController(ISupplierService supplierService, ICategoryService categoryService,
            IAccountAdapter accountAdapter)
        {
            _supplierService = supplierService;
            _categoryService = categoryService;
            _accountAdapter = accountAdapter;
        } 
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            try
            {
                var categories = _categoryService.LoadAllCategories();
                ViewBag.Categories = categories;

                var suppliers = _supplierService.LoadAllSuppliers();
                ViewBag.Suppliers = suppliers;

                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                ViewResponse(ex.Message, ResponseTypes.Danger);

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public ActionResult New(PurchaseCreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return View();
        }
    }
}