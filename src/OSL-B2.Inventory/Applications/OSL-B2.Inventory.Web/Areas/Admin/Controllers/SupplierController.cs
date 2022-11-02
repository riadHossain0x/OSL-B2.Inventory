using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
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
        #endregion
    }
}