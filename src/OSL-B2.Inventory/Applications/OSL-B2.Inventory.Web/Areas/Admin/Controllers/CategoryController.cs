using OSL_B2.Inventory.Service;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _categoryService.AddCategory(new CategoryDto { Name = "Sabbir", IsActive = StatusDto.Active, CreatedBy = 3, CreatedDate = DateTime.Now });
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}