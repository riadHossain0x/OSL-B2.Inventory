using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OSL_B2.Inventory.Service
{
    public interface ICustomerService
    {
        #region Operations
        void AddCustomer(CustomerDto customer);
        void EditCustomer(CustomerDto entity);
        void RemoveCustomer(long id);
        #endregion

        #region Single instances
        Customer GetCustomer(long id);
        #endregion

        #region Load instances
        (int total, int totalDisplay, IList<CustomerDto> records) LoadAllCustomers(string searchBy, int take, int skip, string sortBy, string sortDir); 
        #endregion
    }

    public class CustomerService : ICustomerService
    {
        #region Initialization
        private readonly ICustomerRepository _customerRepostory;

        public CustomerService(ICustomerRepository customerRepostory)
        {
            _customerRepostory = customerRepostory;
        }
        #endregion

        #region Operations
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

            entity.IsActive = Status.Disactive;
            _customerRepostory.Edit(entity);
            _customerRepostory.SaveChanages();
        }
        #endregion

        #region Single instances
        public Customer GetCustomer(long id)
        {
            var entity = _customerRepostory.GetById(id);
            var entityDto = Mapper.Map<Customer>(entity);
            return entityDto;
        }
        #endregion

        #region Load instances
        public (int total, int totalDisplay, IList<CustomerDto> records) LoadAllCustomers(string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            Expression<Func<Customer, bool>> filter = null;
            if (searchBy != null)
            {
                filter = x => x.Name.Contains(searchBy) || x.Email.Contains(searchBy) 
                || x.Mobile.Contains(searchBy) || x.Address.Contains(searchBy);
            }
            var result = _customerRepostory.LoadAll(filter, null, start, length, sortBy, sortDir);

            List<CustomerDto> customers = new List<CustomerDto>();
            foreach (Customer course in result.data)
            {
                customers.Add(Mapper.Map<CustomerDto>(course));
            }

            return (result.total, result.totalDisplay, customers);
        } 
        #endregion
    }
}
