using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class SaleController : AdminBaseController<SaleController>
    {
        private readonly IAccountAdapter _accountAdapter;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;

        public SaleController(IAccountAdapter accountAdapter, ICategoryService categoryService, ICustomerService customerService)
        {
            _accountAdapter = accountAdapter;
            _categoryService = categoryService;
            _customerService = customerService;
        }

        // GET: Admin/Sale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var model = new SaleCreateViewModel();
            model.Customers = _customerService.LoadAllCustomers().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

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