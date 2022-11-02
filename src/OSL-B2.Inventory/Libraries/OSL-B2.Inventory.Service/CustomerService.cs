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
        CustomerDto GetCustomer(long id);
        #endregion

        #region Load instances
        (int total, int totalDisplay, IList<CustomerDto> records) LoadAllCustomers(string searchBy, int take, int skip, string sortBy, string sortDir);
        #endregion
    }

    public class CustomerService : ICustomerService
    {
        #region Initialization
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepostory)
        {
            _customerRepository = customerRepostory;
        }
        #endregion

        #region Operations
        public void AddCustomer(CustomerDto customer)
        {
            try
            {
                var count = _customerRepository.GetCount(x => x.Mobile == customer.Mobile && x.IsActive == Status.Active);

                if (count > 0)
                    throw new InvalidOperationException("There is a customer with same mobile number already exist.");

                var entity = Mapper.Map<Customer>(customer);

                _customerRepository.Add(entity);
                _customerRepository.SaveChanages();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void EditCustomer(CustomerDto entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidOperationException("There is no customer found.");

                var count = _customerRepository.GetCount(x => x.Mobile == entity.Mobile && x.IsActive == Status.Active && x.Id != entity.Id);
                if (count > 0)
                    throw new InvalidOperationException("There is a customer with same name already exist.");

                var customer = Mapper.Map<Customer>(entity);
                _customerRepository.Edit(customer);
                _customerRepository.SaveChanages();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemoveCustomer(long id)
        {
            try
            {
                var entity = _customerRepository.GetById(id);

                if (entity == null)
                    throw new InvalidOperationException("There is no customer found.");

                entity.IsActive = Status.Inactive;
                _customerRepository.Edit(entity);
                _customerRepository.SaveChanages();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Single instances
        public CustomerDto GetCustomer(long id)
        {
            try
            {
                var count = _customerRepository.GetCount(x => x.Id == id && x.IsActive == Status.Active);

                if (count == 0)
                    throw new InvalidOperationException("There is no category found.");

                var entity = _customerRepository.GetById(id);
                var entityDto = Mapper.Map<CustomerDto>(entity);
                return entityDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Load instances
        public (int total, int totalDisplay, IList<CustomerDto> records) LoadAllCustomers(string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            try
            {
                Expression<Func<Customer, bool>> filter = null;
                if (searchBy != null)
                {
                    filter = x => x.Name.Contains(searchBy) || x.Email.Contains(searchBy)
                    || x.Mobile.Contains(searchBy) || x.Address.Contains(searchBy);
                }
                var result = _customerRepository.LoadAll(filter, null, start, length, sortBy, sortDir);

                List<CustomerDto> customers = new List<CustomerDto>();
                foreach (Customer course in result.data)
                {
                    customers.Add(Mapper.Map<CustomerDto>(course));
                }

                return (result.total, result.totalDisplay, customers);
            }
            catch (Exception ex)
            {

                throw;
            }
        } 
        #endregion
    }
}
