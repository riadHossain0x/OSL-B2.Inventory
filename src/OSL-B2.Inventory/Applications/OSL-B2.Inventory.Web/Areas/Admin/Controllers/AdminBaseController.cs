using OSL_B2.Inventory.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminBaseController<TController> : BaseController<TController> where TController : Controller
    {
        public AdminBaseController()
        {
            
        }
    }
}