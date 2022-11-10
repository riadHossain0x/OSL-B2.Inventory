using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Controllers;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using OSL_B2.Inventory.Web.Adapters;
using OSL_B2.Inventory.Service.Exceptions;
using AutoMapper;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CustomerController : AdminBaseController<CustomerController>
    {
        #region initialization
        private readonly IAccountAdapter _accountAdapter;
        private readonly ICustomerService _customerService;

        public CustomerController(IAccountAdapter accountAdapter, ICustomerService customerService)
        {
            Menu(nameof(CustomerController));

            _accountAdapter = accountAdapter;
            _customerService = customerService;
        } 
        #endregion

        #region Manage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCustomers()
        {
            try
            {
                var model = new DataTablesAjaxRequestModel(Request);

                var data = _customerService.LoadAllCustomers(model.SearchText, model.Length, model.Start, model.SortColumn,
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
                                record.Email,
                                record.Mobile,
                                record.Address,
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
                var customer = _customerService.GetCustomer(id);

                var model = new CustomerDetailsViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Mobile = customer.Mobile,
                    Address = customer.Address,
                    CreatedBy = _accountAdapter.FindById(customer.CreatedBy).Email,
                    CreatedDate = customer.CreatedDate,
                    ModifiedBy = _accountAdapter.FindById(customer.ModifiedBy).Email,
                    ModifiedDate = customer.ModifiedDate
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
        public async Task<ActionResult> Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);

                    var customer = model.GetCustomer(user.Id);

                    _customerService.AddCustomer(customer);

                    ViewResponse("Successfully added a new customer.", ResponseTypes.Success);

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
                _customerService.RemoveCustomer(id);

                return Json(ViewResponse("Customer successfully deleted!", string.Empty, ResponseTypes.Success));
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
                var customer = _customerService.GetCustomer(id);

                var model = Mapper.Map<CustomerEditViewModel>(customer);

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
        public ActionResult Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _accountAdapter.FindByName(User.Identity.Name);
                    var customer = model.GetCustomer(user.Id);

                    _customerService.EditCustomer(customer);

                    ViewResponse("Customer successfully updated!", ResponseTypes.Success);

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