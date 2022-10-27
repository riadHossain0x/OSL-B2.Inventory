using AutoMapper;
using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
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
                var userId = await _accountAdapter.GetUserIdAsync();
                var category = model.GetCategory(userId);
                _categoryService.AddCategory(category);
            }

            return View(model);
        }
    }
}