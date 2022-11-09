using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class SaleController : AdminBaseController<SaleController>
    {
        private readonly IAccountAdapter _accountAdapter;
        private readonly ICategoryService _categoryService;

        public SaleController(IAccountAdapter accountAdapter, ICategoryService categoryService)
        {
            _accountAdapter = accountAdapter;
            _categoryService = categoryService;
        }

        // GET: Admin/Sale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var model = new SaleCreateViewModel();
            model.Customers = new List<SelectListItem> { new SelectListItem { Value = "null", Text = "Select a Customer" }, 
                new SelectListItem { Value = "1", Text = "Pen" } };

            var categories = _categoryService.LoadAllCategories();
            ViewBag.Categories = categories;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(SaleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
    }
}