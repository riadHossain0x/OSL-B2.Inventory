using log4net;
using Microsoft.Ajax.Utilities;
using OSL_B2.Inventory.Web.Extensions;
using OSL_B2.Inventory.Web.Models;
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

        #region Helpers
        protected void ViewResponse(string message, ResponseTypes type)
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = message,
                Type = type
            });
        }

        protected object ViewResponse(string message, string code, ResponseTypes type)
        {
            return new { Message = message, Code = code, Type =  type.ToString().ToLower() };
        }
        #endregion
    }
}