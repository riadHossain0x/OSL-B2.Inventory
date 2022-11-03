using AutoMapper;
using log4net;
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
    public interface IProductService
    {
        #region Operations
        void AddProduct(ProductDto entity);
        void EditProduct(ProductDto entity);
        void RemoveProduct(long id);
        #endregion
    }
    public class ProductService : IProductService
    {
        #region Initialization
        public readonly ILog Logger = LogManager.GetLogger("Service");
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Operations
        public void AddProduct(ProductDto entity)
        {
            try
            {
                var count = _productRepository.GetCount(x => x.Name == entity.Name && x.IsActive == Status.Active);

                if (count > 0)
                    throw new InvalidOperationException("There is a product in same category already exist.");

                var product = Mapper.Map<Product>(entity);

                _productRepository.Add(product);
                _productRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void EditProduct(ProductDto entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidOperationException("There is no customer found!");

                var count = _productRepository.GetCount(x => x.Name == entity.Name && x.IsActive == Status.Active && x.Id != entity.Id);
                if (count > 0)
                    throw new InvalidOperationException("There is a product in same category already exist.");

                var product = Mapper.Map<Product>(entity);
                _productRepository.Edit(product);
                _productRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveProduct(long id)
        {
            try
            {
                var entity = _productRepository.GetById(id);

                if (entity == null)
                    throw new InvalidOperationException("There is no product found!");

                entity.IsActive = Status.Inactive;
                _productRepository.Edit(entity);
                _productRepository.SaveChanages();
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
