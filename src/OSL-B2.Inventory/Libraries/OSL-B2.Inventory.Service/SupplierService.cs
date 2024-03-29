﻿using AutoMapper;
using log4net;
using log4net.Repository.Hierarchy;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OSL_B2.Inventory.Service
{
    public interface ISupplierService
    {
        #region Operations
        void AddSupplier(SupplierDto entity);
        void EditSupplier(SupplierDto entity);
        void RemoveSupplier(long id);
        #endregion

        #region Single instances
        SupplierDto GetSupplier(long id);
        #endregion

        #region Load instances
        IList<SupplierDto> LoadAllSuppliers();
        (int total, int totalDisplay, IList<SupplierDto> records) LoadAllSuppliers(string searchBy, int take, int skip, string sortBy, string sortDir);
        #endregion
    }

    public class SupplierService : ISupplierService
    {
        #region Initialization
        public readonly ILog Logger = LogManager.GetLogger("Service");
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        #endregion

        #region Operations
        public void AddSupplier(SupplierDto entity)
        {
            try
            {
                var count = _supplierRepository.GetCount(x => x.Mobile == entity.Mobile && x.IsActive == Status.Active);

                if (count > 0)
                    throw new InvalidOperationException("There is a customer with same mobile number already exist.");

                var supplier = Mapper.Map<Supplier>(entity);

                _supplierRepository.Add(supplier);
                _supplierRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void EditSupplier(SupplierDto entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidOperationException("There is no customer found.");

                var count = _supplierRepository.GetCount(x => x.Mobile == entity.Mobile && x.IsActive == Status.Active && x.Id != entity.Id);
                if (count > 0)
                    throw new InvalidOperationException("There is a customer with same name already exist.");

                var supplier = Mapper.Map<Supplier>(entity);
                _supplierRepository.Edit(supplier);
                _supplierRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveSupplier(long id)
        {
            try
            {
                var entity = _supplierRepository.GetById(id);

                if (entity == null)
                    throw new InvalidOperationException("There is no customer found.");

                entity.IsActive = Status.Inactive;
                _supplierRepository.Edit(entity);
                _supplierRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Single instances
        public SupplierDto GetSupplier(long id)
        {
            try
            {
                var count = _supplierRepository.GetCount(x => x.Id == id && x.IsActive == Status.Active);

                if (count == 0)
                    throw new InvalidOperationException("There is no supplier found.");

                var entity = _supplierRepository.GetById(id);
                var entityDto = Mapper.Map<SupplierDto>(entity);
                return entityDto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Load instances
        public (int total, int totalDisplay, IList<SupplierDto> records) LoadAllSuppliers(string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            try
            {
                Expression<Func<Supplier, bool>> filter = null;
                if (searchBy != null)
                {
                    filter = x => x.Name.Contains(searchBy) || x.Mobile.Contains(searchBy) || x.Address.Contains(searchBy);
                }
                var result = _supplierRepository.LoadAll(filter, null, start, length, sortBy, sortDir);

                List<SupplierDto> suppliers = new List<SupplierDto>();
                foreach (Supplier supplier in result.data)
                {
                    suppliers.Add(Mapper.Map<SupplierDto>(supplier));
                }

                return (result.total, result.totalDisplay, suppliers);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IList<SupplierDto> LoadAllSuppliers()
        {
            try
            {
                var suppliers = _supplierRepository.LoadAll();
                return Mapper.Map<List<SupplierDto>>(suppliers);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion
    }
}
