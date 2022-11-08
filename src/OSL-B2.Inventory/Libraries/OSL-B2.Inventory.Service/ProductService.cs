using AutoMapper;
using log4net;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OSL_B2.Inventory.Service
{
    public interface IProductService
    {
        #region Operations
        void AddProduct(ProductDto entity);
        void EditProduct(ProductDto entity);
        void RemoveProduct(long id);
        #endregion

        #region Single instances
        ProductDto GetProduct(long id);
        ProductDto GetProduct(long id, string includeProperty);
        #endregion

        #region Load instances
        (int total, int totalDisplay, IList<ProductDto> records) LoadAllProducts(string searchBy, int take, int skip, string sortBy, string sortDir);
        IList<ProductDto> LoadAllProducts(long categoryId);
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
                var count = _productRepository.GetCount(x => x.Name == entity.Name && x.CategoryId == entity.CategoryId &&
                                                                                            x.IsActive == Status.Active);

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
                    throw new InvalidOperationException("There is no product found!");

                var count = _productRepository.GetCount(x => x.Name == entity.Name && x.CategoryId == entity.CategoryId
                                                            && x.IsActive == Status.Active && x.Id != entity.Id);
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

        #region Single instances
        public ProductDto GetProduct(long id)
        {
            try
            {
                var count = _productRepository.GetCount(x => x.Id == id && x.IsActive == Status.Active);

                if (count == 0)
                    throw new InvalidOperationException("There is no product found.");

                var entity = _productRepository.GetById(id);
                var entityDto = Mapper.Map<ProductDto>(entity);
                return entityDto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public ProductDto GetProduct(long id, string includeProperty)
        {
            try
            {
                var count = _productRepository.GetCount(x => x.Id == id && x.IsActive == Status.Active);

                if (count == 0)
                    throw new InvalidOperationException("There is no product found.");

                var entity = _productRepository.GetById(id, includeProperty);
                var entityDto = Mapper.Map<ProductDto>(entity);
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
        public (int total, int totalDisplay, IList<ProductDto> records) LoadAllProducts(string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            try
            {
                Expression<Func<Product, bool>> filter = null;
                if (searchBy != null)
                {
                    filter = x => x.Name.Contains(searchBy) || x.Name.Contains(searchBy) || x.Category.Name.Contains(searchBy);
                }
                var result = _productRepository.LoadAll(filter, "Category", start, length, sortBy, sortDir);

                List<ProductDto> products = new List<ProductDto>();
                foreach (Product product in result.data)
                {
                    products.Add(Mapper.Map<ProductDto>(product));
                }

                return (result.total, result.totalDisplay, products);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IList<ProductDto> LoadAllProducts(long categoryId)
        {
            try
            {
                var products = _productRepository.LoadByCategory(categoryId);
                return Mapper.Map<List<ProductDto>>(products);
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
