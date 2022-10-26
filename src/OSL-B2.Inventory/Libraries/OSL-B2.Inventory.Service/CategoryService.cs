using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository;
using System;

namespace OSL_B2.Inventory.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void AddCategory()
        {
            _categoryRepository.Add(new Category { Name = "Shirt", IsActive = Status.Active, CreatedBy = 3, CreatedDate = DateTime.Now });
        }
    }
}
