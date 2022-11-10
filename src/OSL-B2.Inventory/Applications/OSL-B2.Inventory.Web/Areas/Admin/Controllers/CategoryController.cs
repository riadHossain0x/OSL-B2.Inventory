using AutoMapper;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Exceptions;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using OSL_B2.Inventory.Web.Adapters;
using System.Web.Services.Description;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController<CategoryController>
    {
        #region initialization
        private readonly ICategoryService _categoryService;
        private readonly IAccountAdapter _accountAdapter;

        public CategoryController(ICategoryService categoryService, IAccountAdapter accountAdapter)
        {
            Menu(nameof(CategoryController));

            _categoryService = categoryService;
            _accountAdapter = accountAdapter;
        } 
        #endregion

        #region Manage
        public ActionResult Index()
        {
            SubMenu(nameof(Index));

            return View();
        }

        [HttpPost]
        public JsonResult GetCategories()
        {
            try
            {
                var model = new DataTablesAjaxRequestModel(Request);
                var data = _categoryService.LoadAllCategories(model.SearchText, model.Length, model.Start, model.SortColumn, 
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
                                record.IsActive.ToString(),
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

                var category = _categoryService.GetCategory(id);

                var model = new CategoryDetailsViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedBy = _accountAdapter.FindById(category.CreatedBy).Email,
                    CreatedDate = category.CreatedDate,
                    ModifiedBy = _accountAdapter.FindById(category.ModifiedBy).Email,
                    ModifiedDate = category.ModifiedDate
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
        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                _categoryService.RemoveCategory(id);

                return Json(ViewResponse("Category successfully deleted!", string.Empty, ResponseTypes.Success));
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

        public ActionResult Create()
        {
            SubMenu(nameof(Create));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryCreateViewModel model)
        {
            SubMenu(nameof(Create));

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _accountAdapter.FindByNameAsync(User.Identity.Name);

                    var category = model.GetCategory(user.Id);

                    _categoryService.AddCategory(category);

                    ViewResponse("Successfully added a new category.", ResponseTypes.Success);

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

        public ActionResult Edit(long id)
        {
            try
            {
                SubMenu(nameof(Index));

                var category = _categoryService.GetCategory(id);

                var model = Mapper.Map<CategoryEditViewModel>(category);

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
        public ActionResult Edit(CategoryEditViewModel model)
        {
            SubMenu(nameof(Index));

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _accountAdapter.FindByName(User.Identity.Name);
                    var category = model.GetCategory(user.Id);

                    _categoryService.EditCategory(category);

                    ViewResponse("Successfully updated category!", ResponseTypes.Success);

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