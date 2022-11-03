using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using OSL_B2.Inventory.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
                try
                {
                    var fileOperation = new FileOperation(model.ImageFile);
                    if (fileOperation.Validate())
                    {
                        var path = Server.MapPath(ConfigurationManager.AppSettings["ProductImagePath"].ToString());
                        model.Image = fileOperation.SaveFile(path);
                    }

                    var user = _accountAdapter.FindByName(User.Identity.Name);
                    var product = model.GetProduct(user.Id);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewResponse(ex.Message, ResponseTypes.Danger);
                    Logger.Error(ex.Message, ex);
                }
            }

            var categories = _categoryService.LoadAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Categories = categories;

            return View(model);
        }
    }
}