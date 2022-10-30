using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepostory;

        public CustomerService(ICustomerRepository customerRepostory)
        {
            _customerRepostory = customerRepostory;
        }

        public void AddCustomer(CustomerDto customer)
        {
            var count = _customerRepostory.GetCount(x => x.Name == customer.Name && x.IsActive == Status.Active);

            if (count > 0)
                throw new InvalidOperationException("There is a category with same name already exist.");

            var entity = Mapper.Map<Customer>(customer);

            _customerRepostory.Add(entity);
            _customerRepostory.SaveChanages();
        }
    }
}
