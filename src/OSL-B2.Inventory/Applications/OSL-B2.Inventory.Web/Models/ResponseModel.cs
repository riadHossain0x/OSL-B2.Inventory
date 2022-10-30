using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL_B2.Inventory.Web.Models
{
    public enum ResponseTypes
    {
        Success,
        Danger
    }

    public class ResponseModel
    {
        public string Message { get; set; }
        public ResponseTypes Type { get; set; }
    }
}