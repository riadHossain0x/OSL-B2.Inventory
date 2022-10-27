using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Service.Exceptions
{
    public class InnerElementException : Exception
    {
        public InnerElementException(string message) : base(message)
        {

        }
    }
}
