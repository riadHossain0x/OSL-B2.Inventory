using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Controllers
{
    public class BaseController<TController> : Controller where TController : Controller
    {
        public readonly ILog Logger = LogManager.GetLogger(typeof(TController));

        public BaseController()
        {

        }
    }
}