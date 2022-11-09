using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class SaleController : AdminBaseController<SaleController>
    {
        private readonly IAccountAdapter _accountAdapter;

        public SaleController(IAccountAdapter accountAdapter)
        {
            _accountAdapter = accountAdapter;
        }

        // GET: Admin/Sale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var model = new SaleCreateViewModel();
            model.Customers = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "Select a Customer" }, 
                new SelectListItem { Value = "1", Text = "Pen" } };
            return View(model);
        }
    }
}