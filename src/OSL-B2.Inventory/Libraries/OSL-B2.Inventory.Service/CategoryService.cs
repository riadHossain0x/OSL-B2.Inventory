using AutoMapper;
using log4net;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OSL_B2.Inventory.Service
{
    public interface ICategoryService
    {
        #region Operations
        void AddCategory(CategoryDto item);
        void EditCategory(CategoryDto entity);
        void RemoveCategory(long id);
        #endregion

        #region Load instances
        IList<CategoryDto> LoadAllCategories();
        (int total, int totalDisplay, IList<CategoryDto> records) LoadAllCategories(string searchBy, int length, int start, string sortBy, string sortDir);
        #endregion

        #region Single instances
        CategoryDto GetCategory(long id);
        #endregion
    }

    public class CategoryService : ICategoryService
    {
        #region Initialization
        public readonly ILog Logger = LogManager.GetLogger("Service");
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        #region Load instances
        public (int total, int totalDisplay, IList<CategoryDto> records) LoadAllCategories(string searchBy = null, int length = 10, int start = 1, string sortBy = null, string sortDir = null)
        {
            try
            {
                Expression<Func<Category, bool>> filter = null;
                if (searchBy != null)
                {
                    filter = x => x.Name.Contains(searchBy);
                }
                var result = _categoryRepository.LoadAll(filter, null, start, length, sortBy, sortDir);

                List<CategoryDto> customers = new List<CategoryDto>();
                foreach (Category course in result.data)
                {
                    customers.Add(Mapper.Map<CategoryDto>(course));
                }

                return (result.total, result.totalDisplay, customers);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IList<CategoryDto> LoadAllCategories()
        {
            try
            {
                var entities = _categoryRepository.LoadAll();
                return Mapper.Map<List<CategoryDto>>(entities);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Operations
        public void AddCategory(CategoryDto item)
        {
            try
            {
                var count = _categoryRepository.GetCount(x => x.Name == item.Name && x.IsActive == Status.Active);

                if (count > 0)
                    throw new InvalidOperationException("There is a category with same name already exist.");

                var entity = Mapper.Map<Category>(item);

                _categoryRepository.Add(entity);
                _categoryRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void EditCategory(CategoryDto entity)
        {
            try
            {
                if (entity == null)
                    throw new InvalidOperationException("There is no category found.");

                var count = _categoryRepository.GetCount(x => x.Name == entity.Name && x.IsActive == Status.Active && x.Id != entity.Id);
                if (count > 0)
                    throw new InvalidOperationException("There is a category with same name already exist.");

                var category = Mapper.Map<Category>(entity);
                _categoryRepository.Edit(category);
                _categoryRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveCategory(long id)
        {
            try
            {
                var entity = _categoryRepository.GetById(id, "Products");

                if (entity == null)
                    throw new InvalidOperationException("There is no category found.");

                if (entity.Products.Count > 0)
                    throw new InnerElementException($"There are same products under '{entity.Name}' category. Please delete those product and try again.");

                entity.IsActive = Status.Inactive;
                _categoryRepository.Edit(entity);
                _categoryRepository.SaveChanages();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region Single instances
        public CategoryDto GetCategory(long id)
        {
            try
            {
                var count = _categoryRepository.GetCount(x => x.Id == id && x.IsActive == Status.Active);

                if (count == 0)
                    throw new InvalidOperationException("There is no category found.");

                var entity = _categoryRepository.GetById(id);
                var entityDto = Mapper.Map<CategoryDto>(entity);
                return entityDto;
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
