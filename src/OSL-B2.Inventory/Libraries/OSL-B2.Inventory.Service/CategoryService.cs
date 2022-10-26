using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using OSL_B2.Inventory.Service.Dtos;
using System;
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

        public void RemoveCategory(long id)
        {
            var entity = _categoryRepository.GetById(id);

            if (entity == null)
                throw new InvalidOperationException("Category not found.");

            _categoryRepository.Remove(entity);
            _categoryRepository.SaveChanages();
        }
    }
}
