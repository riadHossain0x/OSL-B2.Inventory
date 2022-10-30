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
        Category GetById(long id, string includeProperty = null);
        int GetCount(Expression<Func<Category, bool>> filter = null);
        void Add(Category entity);
        void Edit(Category entity);
        void Remove(Category entity);
        void SaveChanages();
        Task SaveChanagesAsync();
    }
}
