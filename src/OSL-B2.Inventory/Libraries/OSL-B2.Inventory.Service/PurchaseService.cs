using AutoMapper;
using log4net;
using log4net.Repository.Hierarchy;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Repository.DbContexts;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Service
{
    public interface IPurchaseService
    {
        #region Load instances
        (int total, int totalDisplay, IList<PurchaseDto> records) LoadAllPurchases(string searchBy, int take, int skip, string sortBy, string sortDir);
        #endregion

        #region Operations
        void AddPurchase(PurchaseDto entity, List<StockUpdateDto> stocks);
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

        #region Load instances
        public (int total, int totalDisplay, IList<PurchaseDto> records) LoadAllPurchases(string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            try
            {
                Expression<Func<Purchase, bool>> filter = null;
                if (searchBy != null)
                {
                    filter = x => x.PurchaseNo.Contains(searchBy);
                }
                var result = _purchaseRepository.LoadAll(filter, string.Empty, start, length, sortBy, sortDir);

                List<PurchaseDto> purchases = new List<PurchaseDto>();
                foreach (Purchase purchase in result.data)
                {
                    purchases.Add(Mapper.Map<PurchaseDto>(purchase));
                }

                return (result.total, result.totalDisplay, purchases);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Operations
        public void AddPurchase(PurchaseDto entity, List<StockUpdateDto> stocks)
        {
            try
            {
                var count = _purchaseRepository.GetCount(x => x.PurchaseNo == entity.PurchaseNo &&
                                                                                            x.IsActive == Status.Active);

                if (count > 0)
                    throw new InvalidOperationException("There is a purchase with same Purchase No already exist.");

                var purchase = Mapper.Map<Purchase>(entity);

                using (var context = new IMSDbContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var purchaseEO = context.Purchases.Add(purchase);
                            context.SaveChanges();

                            foreach (var stock in stocks)
                            {
                                var product = context.Products.Find(stock.ProductId);
                                product.BuyingPrice = ((product.BuyingPrice * product.Quantity) + (stock.BuyingPrice * stock.Quantity)) / (product.Quantity + stock.Quantity);
                                product.SalePrice = ((product.SalePrice * product.Quantity) + (stock.SalePrice * stock.Quantity)) / (product.Quantity + stock.Quantity);
                                product.Quantity += stock.Quantity;
                                context.Products.AddOrUpdate(product);
                                context.SaveChanges();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            Logger.Error(ex.Message, ex);

                            throw;
                        }
                    }
                };
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
                    throw new InvalidOperationException("There is a purchase with same Purchase No already exist.");

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
