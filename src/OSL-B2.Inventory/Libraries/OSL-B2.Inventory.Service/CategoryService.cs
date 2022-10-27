using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OSL_B2.Inventory.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void AddCategory(CategoryDto item)
        {
            var count = _categoryRepository.GetCount(x => x.Name == item.Name);

            if (count > 0)
                throw new InvalidOperationException("Category name already exist.");

            var entity = Mapper.Map<Category>(item);

            _categoryRepository.Add(entity);
            _categoryRepository.SaveChanages();
        }

        public void EditCategory(CategoryDto entity)
        {
            if(entity == null)
                throw new InvalidOperationException("Category not found.");

            var count = _categoryRepository.GetCount(x => x.Name == entity.Name && x.Id != entity.Id);
            if (count > 0)
                throw new InvalidOperationException("Category with same name already exist.");

            var category = Mapper.Map<Category>(entity);
            _categoryRepository.Edit(category);
            _categoryRepository.SaveChanages();
        }

        public IList<CategoryDto> GetAllCategories()
        {
            var entities = _categoryRepository.GetAll();
            var entitiesDto = Mapper.Map<IList<CategoryDto>>(entities);
            return entitiesDto;
        }

        public CategoryDto GetCategory(long id)
        {
            var entity = _categoryRepository.GetById(id);
            var entityDto = Mapper.Map<CategoryDto>(entity);
            return entityDto;
        }

        public void RemoveCategory(long id)
        {
            var entity = _categoryRepository.GetById(id);

            if (entity == null)
                throw new InvalidOperationException("Category not found.");

            entity.IsActive = Status.Disactive;
            _categoryRepository.Edit(entity);
            _categoryRepository.SaveChanages();
        }
    }
}
