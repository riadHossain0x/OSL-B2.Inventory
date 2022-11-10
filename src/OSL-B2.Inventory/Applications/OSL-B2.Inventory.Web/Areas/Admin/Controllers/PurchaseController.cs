﻿using AutoMapper;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Service.Exceptions;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class PurchaseController : AdminBaseController<PurchaseController>
    {
        #region Initialization
        private readonly IAccountAdapter _accountAdapter;
        private readonly IPurchaseService _purchaseService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public PurchaseController(IPurchaseService purchaseService, ISupplierService supplierService, ICategoryService categoryService,
            IProductService productService, IAccountAdapter accountAdapter)
        {
            Menu(nameof(PurchaseController));

            _purchaseService = purchaseService;
            _supplierService = supplierService;
            _categoryService = categoryService;
            _productService = productService;
            _accountAdapter = accountAdapter;
        }
        #endregion

        #region Manage
        public ActionResult Index()
        {
            SubMenu(nameof(Index));

            return View();
        }

        public JsonResult GetPurchases(string filter)
        {
            try
            {
                DateTime startDate = default;
                DateTime endDate = default;

                if (filter != null)
                {
                    var dates = filter.Split('-');
                    startDate = DateTime.Parse(dates[0]);
                    endDate = DateTime.Parse(dates[1]);
                }
                var model = new DataTablesAjaxRequestModel(Request);

                var data = _purchaseService.LoadAllPurchases(startDate, endDate, model.SearchText, model.Length, model.Start, model.SortColumn,
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
                                record.PurchaseNo,
                                record.PurchaseDate.ToShortDateString(),
                                record.GrandTotal.ToString(),
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

        public ActionResult Details(long id)
        {
            try
            {
                SubMenu(nameof(Index));

                var purchases = _purchaseService.GetPurchase(id, "PurchaseDetails");
                var model = Mapper.Map<PurchaseDetailViewModel>(purchases);
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                ViewResponse(ex.Message, ResponseTypes.Danger);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Operations
        public ActionResult New()
        {
            try
            {
                SubMenu(nameof(New));

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
        public async Task<ActionResult> New(PurchaseCreateViewModel model)
        {
            SubMenu(nameof(New));

            if (ModelState.IsValid)
            {
                try
                {
                    var purchaseDetails = new List<PurchaseDetailDto>();
                    var stocks = new List<StockUpdateDto>();

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

                        stocks.Add(new StockUpdateDto
                        {
                            ProductId = model.ProductId[i],
                            Quantity = model.Quantity[i],
                            BuyingPrice = model.Price[i],
                            SalePrice = model.SalePrice[i],
                        });
                    }

                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);
                    var purchase = Mapper.Map<PurchaseDto>(model);
                    purchase.PurchaseDetails = purchaseDetails;
                    purchase.CreatedBy = purchase.ModifiedBy = user.Id;
                    purchase.CreatedDate = purchase.ModifiedDate = DateTime.Now;

                    _purchaseService.AddPurchase(purchase, stocks);

                    ViewResponse("Successfully add a new purchase.", ResponseTypes.Success);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                    ViewResponse(ex.Message, ResponseTypes.Danger);
                }
            }
            else { ViewResponse("Failed to process request, please check you inputs.", ResponseTypes.Danger); }    

            var categories = _categoryService.LoadAllCategories();
            ViewBag.Categories = categories;

            var suppliers = _supplierService.LoadAllSuppliers();
            ViewBag.Suppliers = suppliers;

            return View();
        }

        [HttpPost]
        public JsonResult GetProducts(long categoryId)
        {
            try
            {
                var products = _productService.LoadAllProducts(categoryId).Select(x => new { value = x.Id, text = x.Name, qty = x.Quantity, price = x.SalePrice, buyingPrice = x.BuyingPrice });
                return Json(products);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                return Json(ViewResponse(ex.Message, string.Empty, ResponseTypes.Danger));
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                _purchaseService.RemovePurchase(id);

                return Json(ViewResponse("Purchase successfully deleted!", string.Empty, ResponseTypes.Success));
            }
            catch (InnerElementException ie)
            {
                return Json(ViewResponse(ie.Message, string.Empty, ResponseTypes.Danger));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                return Json(ViewResponse(ex.Message, string.Empty, ResponseTypes.Danger));
            }
        }
        #endregion
    }
}