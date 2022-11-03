using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using OSL_B2.Inventory.Web.Utilities;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController<ProductController>
    {
        #region Initialization
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountAdapter _accountAdapter;

        public ProductController(IProductService productService ,ICategoryService categoryService, IAccountAdapter accountAdapter)
        {
            _productService = productService;
            _categoryService = categoryService;
            _accountAdapter = accountAdapter;
        }
        #endregion

        #region Manage
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult GetProducts()
        {
            try
            {
                var model = new DataTablesAjaxRequestModel(Request);

                var data = _productService.LoadAllProducts(model.SearchText, model.Length, model.Start, model.SortColumn,
                    model.SortDirection);

                var count = 1;

                return Json(new
                {
                    draw = Request["draw"],
                    recordsTotal = data.total,
                    recordsFiltered = data.totalDisplay,
                    data = (from record in data.records
                            select new string[]
                            {
                                count++.ToString(),
                                string.Concat(ConfigurationManager.AppSettings["ProductImagePath"].ToString(),
                                            record.Image),
                                record.Name,
                                record.Category.Name,
                                record.Details,
                                _accountAdapter.FindById(record.ModifiedBy).Email,
                                record.ModifiedDate.ToString(),
                                _accountAdapter.FindById(record.CreatedBy).Email,
                                record.CreatedDate.ToString(),
                                record.Id.ToString()
                            }
                        ).ToArray()
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return default(JsonResult);
        }
        #endregion

        #region Operations
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

                    _productService.AddProduct(product);

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
        #endregion
    }
}