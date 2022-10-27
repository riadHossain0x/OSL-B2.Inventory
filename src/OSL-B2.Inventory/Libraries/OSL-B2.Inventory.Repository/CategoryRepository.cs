using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMSDbContext _context;

        public CategoryRepository(IMSDbContext context)
        {
            _context = context;
        }
        public void Add(Category entity)
        {
            if (entity != null)
                _context.Categories.Add(entity);
        }

        public void Remove(Category entity) => _context.Categories.Remove(entity);

        public IList<Category> GetAll()
        {
            IQueryable<Category> query = _context.Categories;
            query = query.Where(x => x.IsActive == Status.Active);
            return query.ToList();
        }

        public Category GetById(long id) => _context.Categories.Find(id);

        public Category GetById(long id, string includeProperties = null)
        {
            IQueryable<Category> query = _context.Categories;
            query.Where(x => x.Id == id);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
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

        public void Edit(Category entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Categories.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
    }
}
