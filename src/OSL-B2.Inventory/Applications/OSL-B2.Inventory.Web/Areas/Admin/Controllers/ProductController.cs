using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController<ProductController>
    {
        #region Initialization
        private readonly ICategoryService _categoryService;
        private readonly IAccountAdapter _accountAdapter;

        public ProductController(ICategoryService categoryService, IAccountAdapter accountAdapter)
        {
            _categoryService = categoryService;
            _accountAdapter = accountAdapter;
        } 
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new ProductCreateViewModel();

            var categories = _categoryService.LoadAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}