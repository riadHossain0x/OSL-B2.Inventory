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

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController<CategoryController>
    {
        #region initialization
        private readonly ICategoryService _categoryService;
        private readonly IAccountAdapter _accountAdapter;

        public CategoryController(ICategoryService categoryService, IAccountAdapter accountAdapter)
        {
            _categoryService = categoryService;
            _accountAdapter = accountAdapter;
        } 
        #endregion

        #region Manage
        public ActionResult Index()
        {
            var model = _categoryService.LoadAllCategories().Select(x => new CategoryListViewModel { Id = x.Id, Name = x.Name });
            return View(model);
        }

        public ActionResult Details(long id)
        {
            var category = _categoryService.GetCategory(id);

            var model = new CategoryDetailsViewModel
            {
                Name = category.Name,
                CreatedBy = _accountAdapter.FindById(category.CreatedBy).Email,
                CreatedDate = category.CreatedDate,
                ModifiedBy = _accountAdapter.FindById(category.ModifiedBy).Email,
                ModifiedDate = category.ModifiedDate
            };

            return View(model);
        }
        #endregion

        #region Operations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                _categoryService.RemoveCategory(id);

                ViewResponse("Category successfully deleted.", ResponseTypes.Success);
            }
            catch (InnerElementException ie)
            {
                ViewResponse(ie.Message, ResponseTypes.Danger);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                ViewResponse(ex.Message, ResponseTypes.Danger);
            }

            return RedirectToAction(nameof(Index), new { area = "Admin" });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryCreateViewModel model)
        {
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
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _accountAdapter.FindByName(User.Identity.Name);
                    var category = model.GetCategory(user.Id);

                    _categoryService.EditCategory(category);

                    ViewResponse("Successfully updated category.", ResponseTypes.Success);

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