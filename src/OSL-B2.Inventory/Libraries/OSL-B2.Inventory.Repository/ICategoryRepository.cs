using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository
{
    public interface ICategoryRepository
    {
        IList<Category> GetAll();
        Category GetById(long id);
        int GetCount(Expression<Func<Category, bool>> filter = null);
        long Add(Category entity);
        long Update(Category entity);
        void Delete(long id);
    }
}
