using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OSL_B2.Inventory.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMSDbContext _context;

        public CategoryRepository(IMSDbContext context)
        {
            _context = context;
        }
        public long Add(Category entity)
        {
            long result = -1;

            if (entity != null)
            {
                _context.Categories.Add(entity);
                _context.SaveChanges();
                result = entity.Id;
            }

            return result;
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(long id)
        {
            return _context.Categories.Find(id);
        }

        public int GetCount(Expression<Func<Category, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Category> query = _context.Categories;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }

        public long Update(Category entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
