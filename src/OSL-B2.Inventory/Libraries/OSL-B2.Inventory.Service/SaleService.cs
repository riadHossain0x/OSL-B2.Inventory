using AutoMapper;
using log4net;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository.DbContexts;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace OSL_B2.Inventory.Service
{
    public interface ISaleService
    {
        #region Load instances
        (int total, int totalDisplay, IList<SaleDto> records) LoadAllSales(DateTime startDate, DateTime endDate, string searchBy, int take, int skip, string sortBy, string sortDir);
        #endregion

        #region Single instances
        SaleDto GetSale(long id, string includeProperty);
        #endregion

        #region Operations
        void AddSale(SaleDto entity, List<StockUpdateDto> stocks);
        void EditSale(SaleDto entity);
        void RemoveSale(long id);
        #endregion
    }
    public class SaleService : ISaleService
    {
        #region Initialization
        public readonly ILog Logger = LogManager.GetLogger("Service");
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        #endregion

        #region Load instances
        public (int total, int totalDisplay, IList<SaleDto> records) LoadAllSales(DateTime startDate, DateTime endDate, string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            try
            {
                Expression<Func<Sale, bool>> searchFilter = null;
                Expression<Func<Sale, bool>> dateFilter = null;
                if (searchBy != null)
                {
                    searchFilter = x => x.Customer.Name.Contains(searchBy);
                }
                if (startDate != default && endDate != default)
                {
                    dateFilter = (x => x.SaleDate >= startDate && x.SaleDate <= endDate);
                }

                var result = _saleRepository.LoadAll(dateFilter, searchFilter, string.Empty, start, length, sortBy, sortDir);

                List<SaleDto> sales = new List<SaleDto>();
                foreach (var sale in result.data)
                {
                    sales.Add(Mapper.Map<SaleDto>(sale));
                }

                return (result.total, result.totalDisplay, sales);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Single instances
        public SaleDto GetSale(long id, string includeProperty)
        {
            try
            {
                var count = _saleRepository.GetCount(x => x.Id == id && x.IsActive == Status.Active);

                if (count == 0)
                    throw new InvalidOperationException("There is no purchase found.");

                var entity = _saleRepository.GetById(id, includeProperty);
                var entityDto = Mapper.Map<SaleDto>(entity);
                return entityDto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Operations
        public void AddSale(SaleDto entity, List<StockUpdateDto> stocks)
        {
            try
            {
                var sale = Mapper.Map<Sale>(entity);

                using (var context = new IMSDbContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var purchaseEO = context.Sales.Add(sale);
                            context.SaveChanges();

                            foreach (var stock in stocks)
                            {
                                var product = context.Products.Find(stock.ProductId);
                                product.Quantity -= stock.Quantity;
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

        public void EditSale(SaleDto entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidOperationException("There is no purchase found!");

                var sale = Mapper.Map<Sale>(entity);
                _saleRepository.Edit(sale);
                _saleRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveSale(long id)
        {
            try
            {
                var entity = _saleRepository.GetById(id);

                if (entity == null)
                    throw new InvalidOperationException("There is no purchase found!");

                entity.IsActive = Status.Inactive;
                _saleRepository.Edit(entity);
                _saleRepository.SaveChanages();
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
