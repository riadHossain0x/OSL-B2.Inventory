﻿using AutoMapper;
using log4net;
using log4net.Repository.Hierarchy;
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
    public interface IPurchaseService
    {
        #region Operations
        void AddPurchase(PurchaseDto entity);
        void EditPurchase(PurchaseDto entity);
        void RemovePurchase(long id);
        #endregion
    }

    public class PurchaseService : IPurchaseService
    {
        #region Initialization
        public readonly ILog Logger = LogManager.GetLogger("Service");
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        } 
        #endregion

        #region Operations
        public void AddPurchase(PurchaseDto entity)
        {
            try
            {
                var count = _purchaseRepository.GetCount(x => x.PurchaseNo == entity.PurchaseNo &&
                                                                                            x.IsActive == Status.Active);

                if (count > 0)
                    throw new InvalidOperationException("There is a purchase with same Purchase Number already exist.");

                var purchase = Mapper.Map<Purchase>(entity);

                _purchaseRepository.Add(purchase);
                _purchaseRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void EditPurchase(PurchaseDto entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidOperationException("There is no purchase found!");

                var count = _purchaseRepository.GetCount(x => x.PurchaseNo == entity.PurchaseNo && x.IsActive == Status.Active && 
                                                                                                        x.Id != entity.Id);
                if (count > 0)
                    throw new InvalidOperationException("There is a purchase with same Purchase Number already exist.");

                var purchase = Mapper.Map<Purchase>(entity);
                _purchaseRepository.Edit(purchase);
                _purchaseRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemovePurchase(long id)
        {
            try
            {
                var entity = _purchaseRepository.GetById(id);

                if (entity == null)
                    throw new InvalidOperationException("There is no purchase found!");

                entity.IsActive = Status.Inactive;
                _purchaseRepository.Edit(entity);
                _purchaseRepository.SaveChanages();
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
