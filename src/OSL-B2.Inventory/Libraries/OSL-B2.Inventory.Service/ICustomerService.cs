using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Service
{
    public interface ICustomerService
    {
        void AddCustomer(CustomerDto customer);
        void EditCustomer(CustomerDto entity);
        void RemoveCustomer(long id);
        Customer GetCustomer(long id);
        IList<CustomerDto> GetAllCustomers();
    }
}
