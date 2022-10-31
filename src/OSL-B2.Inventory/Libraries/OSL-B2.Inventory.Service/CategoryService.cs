using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Service.Exceptions;
using System;
using System.Collections.Generic;

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
        #endregion

        #region Single instances
        CategoryDto GetCategory(long id); 
        #endregion
    }

    public class CategoryService : ICategoryService
    {
        #region Initialization
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        #region Load instances
        public IList<CategoryDto> LoadAllCategories()
        {
            var entities = _categoryRepository.LoadAll();
            var entitiesDto = Mapper.Map<IList<CategoryDto>>(entities);
            return entitiesDto;
        }
        #endregion

        #region Operations
        public void AddCategory(CategoryDto item)
        {
            var count = _categoryRepository.GetCount(x => x.Name == item.Name && x.IsActive == Status.Active);

            if (count > 0)
                throw new InvalidOperationException("There is a category with same name already exist.");

            var entity = Mapper.Map<Category>(item);

            _categoryRepository.Add(entity);
            _categoryRepository.SaveChanages();
        }

        public void EditCategory(CategoryDto entity)
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

        public void RemoveCategory(long id)
        {
            var entity = _categoryRepository.GetById(id, "Products");

            if (entity == null)
                throw new InvalidOperationException("There is no category found.");

            if (entity.Products.Count > 0)
                throw new InnerElementException($"There are same products under '{entity.Name}' category. Please delete those product and try again.");

            entity.IsActive = Status.Disactive;
            _categoryRepository.Edit(entity);
            _categoryRepository.SaveChanages();
        }
        #endregion

        #region Single instances
        public CategoryDto GetCategory(long id)
        {
            var entity = _categoryRepository.GetById(id);
            var entityDto = Mapper.Map<CategoryDto>(entity);
            return entityDto;
        } 
        #endregion

    }
}
