using OSL_B2.Inventory.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _categoryService.AddCategory();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}