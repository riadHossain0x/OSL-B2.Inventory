using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;

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
                throw new InvalidOperationException("There is a customer with same name already exist.");

            var entity = Mapper.Map<Customer>(customer);

            _customerRepostory.Add(entity);
            _customerRepostory.SaveChanages();
        }

        public void EditCustomer(CustomerDto entity)
        {
            if (entity == null)
                throw new InvalidOperationException("There is no customer found.");

            var count = _customerRepostory.GetCount(x => x.Name == entity.Name && x.IsActive == Status.Active && x.Id != entity.Id);
            if (count > 0)
                throw new InvalidOperationException("There is a customer with same name already exist.");

            var customer = Mapper.Map<Customer>(entity);
            _customerRepostory.Edit(customer);
            _customerRepostory.SaveChanages();
        }

        public void RemoveCustomer(long id)
        {
            var entity = _customerRepostory.GetById(id);

            if (entity == null)
                throw new InvalidOperationException("There is no customer found.");

            //if (entity.Products.Count > 0)
            //    throw new InnerElementException($"There are same products under '{entity.Name}' category. Please delete those product and try again.");

            entity.IsActive = Status.Disactive;
            _customerRepostory.Edit(entity);
            _customerRepostory.SaveChanages();
        }

        public Customer GetCustomer(long id)
        {
            var entity = _customerRepostory.GetById(id);
            var entityDto = Mapper.Map<Customer>(entity);
            return entityDto;
        }

        public IList<CustomerDto> GetAllCustomers()
        {
            var entities = _customerRepostory.GetAll();
            var entitiesDto = Mapper.Map<IList<CustomerDto>>(entities);
            return entitiesDto;
        }
    }
}
