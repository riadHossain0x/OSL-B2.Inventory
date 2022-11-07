using AutoMapper;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Dtos;
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
        private readonly IProductService _productService;

        public PurchaseController(ISupplierService supplierService, ICategoryService categoryService,
            IProductService productService, IAccountAdapter accountAdapter)
        {
            _supplierService = supplierService;
            _categoryService = categoryService;
            _productService = productService;
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
        public ActionResult New(PurchaseCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var purchaseDetails = new List<PurchaseDetailDto>();
                for (int i = 0; i < model.Total.Count; i++)
                {
                    purchaseDetails.Add(new PurchaseDetailDto
                    {
                        SupplierId = model.SupplierId[i],
                        ProductId = model.ProductId[i],
                        Quantity = model.Quantity[i],
                        Price = model.Price[i],
                        Total = model.Total[i],
                    });
                }

                var purchase = Mapper.Map<PurchaseDto>(model);
                purchase.PurchaseDetails = purchaseDetails;


                return View(model);
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetProducts(long categoryId)
        {
            try
            {
                var products = _productService.LoadAllProducts(categoryId).Select(x => new {value = x.Id, text = x.Name});
                return Json(products);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                return Json(ViewResponse(ex.Message, string.Empty, ResponseTypes.Danger));
            }
        }
    }
}