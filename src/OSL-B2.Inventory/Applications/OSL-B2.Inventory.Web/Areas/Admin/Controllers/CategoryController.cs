using AutoMapper;
using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Web.Extensions;
using OSL_B2.Inventory.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController<CategoryController>
    {
        private readonly ICategoryService _categoryService;
        private readonly IAccountAdapter _accountAdapter;

        public CategoryController(ICategoryService categoryService, IAccountAdapter accountAdapter)
        {
            _categoryService = categoryService;
            _accountAdapter = accountAdapter;
        }

        public ActionResult Index()
        {
            var model = _categoryService.GetAllCategories().Select(x => new CategoryListViewModel { Id = x.Id, Name = x.Name });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                _categoryService.RemoveCategory(id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
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

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new category.",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction(nameof(Index), new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
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

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                }
            }
            return View(model);
        }
    }
}