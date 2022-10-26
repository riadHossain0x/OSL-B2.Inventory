using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Service
{
    public interface ICategoryService
    {
        void AddCategory(CategoryDto item);
        void RemoveCategory(long id);
        IList<CategoryDto> GetAllCategories();
    }
}
