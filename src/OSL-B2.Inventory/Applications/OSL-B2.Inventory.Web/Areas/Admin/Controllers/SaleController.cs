using AutoMapper;
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
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class SaleController : AdminBaseController<SaleController>
    {
        #region Initialization
        private readonly IAccountAdapter _accountAdapter;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        private readonly ISaleService _saleService;

        public SaleController(IAccountAdapter accountAdapter, ICategoryService categoryService, ICustomerService customerService,
            ISaleService saleService)
        {
            Menu(nameof(SaleController));

            _accountAdapter = accountAdapter;
            _categoryService = categoryService;
            _customerService = customerService;
            _saleService = saleService;
        }
        #endregion

        #region Manage
        // GET: Admin/Sale
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSales(string filter)
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

                var data = _saleService.LoadAllSales(startDate, endDate, model.SearchText, model.Length, model.Start, model.SortColumn,
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
                                _customerService.GetCustomer(record.CustomerId).Name,
                                record.SaleDate.ToShortDateString(),
                                record.GrandTotal.ToString(),
                                record.DiscountTotal.ToString(),
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
                var sale = _saleService.GetSale(id, "SaleDetails");
                var model = Mapper.Map<SaleDetailViewModel>(sale);
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
            var model = new SaleCreateViewModel();
            model.Customers = _customerService.LoadAllCustomers().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = string.Format($"{x.Name} - {x.Mobile}")
            }).ToList();
            model.Customers.Insert(0, new SelectListItem { Value = "-1", Text = "Select a Customer" });

            var categories = _categoryService.LoadAllCategories();
            ViewBag.Categories = categories;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(SaleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var saleDetails = new List<SaleDetailDto>();
                    var stocks = new List<StockUpdateDto>();

                    for (int i = 0; i < model.ProductId.Count; i++)
                    {
                        saleDetails.Add(new SaleDetailDto
                        {
                            ProductId = model.ProductId[i],
                            Quantity = model.Quantity[i],
                            SalePrice = model.Price[i],
                            BuyingPrice = model.BuyingPrice[i],
                            Total = model.Total[i]
                        });

                        stocks.Add(new StockUpdateDto
                        {
                            ProductId = model.ProductId[i],
                            Quantity = model.Quantity[i]
                        });
                    }

                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);

                    var sale = Mapper.Map<SaleDto>(model);
                    sale.SaleDetails = saleDetails;
                    sale.CreatedBy = sale.ModifiedBy = user.Id;
                    sale.CreatedDate = sale.ModifiedDate = DateTime.Now;

                    _saleService.AddSale(sale, stocks);

                    ViewResponse("Successfully add a new sale.", ResponseTypes.Success);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                    ViewResponse(ex.Message, ResponseTypes.Danger);
                }
            }
            else { ViewResponse("Failed to process request, please check you inputs.", ResponseTypes.Danger); }

            model.Customers = _customerService.LoadAllCustomers().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            model.Customers.Insert(0, new SelectListItem { Value = "-1", Text = "Select a Customer" });

            var categories = _categoryService.LoadAllCategories();
            ViewBag.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                _saleService.RemoveSale(id);

                return Json(ViewResponse("Sale successfully deleted!", string.Empty, ResponseTypes.Success));
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