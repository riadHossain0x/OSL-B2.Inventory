using AutoMapper;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Exceptions;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        #region Manage
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSuppliers()
        {
            try
            {
                var model = new DataTablesAjaxRequestModel(Request);

                var data = _supplierService.LoadAllSuppliers(model.SearchText, model.Length, model.Start, model.SortColumn,
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
                                record.Name,
                                record.Mobile,
                                record.Address,
                                record.Details != null ? record.Details.Length < 10 ? record.Details : string.Concat(record.Details.Substring(0, 10), "...") : record.Details, 
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
                var supplier = _supplierService.GetSupplier(id);

                var model = new SupplierDetailsViewModel
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Mobile = supplier.Mobile,
                    Address = supplier.Address,
                    Details = supplier.Details,
                    CreatedBy = _accountAdapter.FindById(supplier.CreatedBy).Email,
                    CreatedDate = supplier.CreatedDate,
                    ModifiedBy = _accountAdapter.FindById(supplier.ModifiedBy).Email,
                    ModifiedDate = supplier.ModifiedDate
                };

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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SupplierCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);

                    var supplier = model.GetSupplier(user.Id);

                    _supplierService.AddSupplier(supplier);

                    ViewResponse("Successfully added a new supplier.", ResponseTypes.Success);

                    return RedirectToAction(nameof(Index), new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);

                    ViewResponse(ex.Message, ResponseTypes.Danger);
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                _supplierService.RemoveSupplier(id);

                return Json(ViewResponse("Supplier successfully deleted!", string.Empty, ResponseTypes.Success));
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

        public ActionResult Edit(long id)
        {
            try
            {
                var supplier = _supplierService.GetSupplier(id);

                var model = Mapper.Map<SupplierEditViewModel>(supplier);

                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                ViewResponse(ex.Message, ResponseTypes.Danger);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _accountAdapter.FindByName(User.Identity.Name);
                    var supplier = model.GetSupplier(user.Id);

                    _supplierService.EditSupplier(supplier);
                    ViewResponse("Supplier successfully updated!", ResponseTypes.Success);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);

                    ViewResponse(ex.Message, ResponseTypes.Danger);
                }
            }
            return View(model);
        }
        #endregion
    }
}