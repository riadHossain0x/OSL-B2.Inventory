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
    public interface ICategoryRepository
    {
        #region Load instances
        IList<Category> LoadAll();
        (IList<Category> data, int total, int totalDisplay) LoadAll(Expression<Func<Category, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null);
        #endregion

        #region Single instances
        Category GetById(long id);
        Category GetById(long id, string includeProperty = null);
        #endregion

        #region Operations
        void Add(Category entity);
        void Edit(Category entity);
        void Remove(Category entity);
        void SaveChanages();
        Task SaveChanagesAsync();
        #endregion

        #region Others
        int GetCount(Expression<Func<Category, bool>> filter = null);
        #endregion
    }

    public class CategoryRepository : ICategoryRepository
    {
        #region Initializations
        private readonly IMSDbContext _context;

        public CategoryRepository(IMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Load instances
        public IList<Category> LoadAll()
        {
            IQueryable<Category> query = _context.Categories;
            query = query.Where(x => x.IsActive == Status.Active);
            return query.ToList();
        }

        public (IList<Category> data, int total, int totalDisplay) LoadAll(Expression<Func<Category, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null)
        {
            IQueryable<Category> query = _context.Categories;

            query = query.Where(x => x.IsActive == Status.Active);

            var total = query.Count();
            var totalDisplay = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }

            //sorting
            switch (sortBy)
            {
                case "Name":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
                    break;
            }

            var result = query.OrderBy(x => x.Name).Skip(pageIndex).Take(pageSize);

            return (result.ToList(), total, totalDisplay);
        }
        #endregion

        #region Single instances
        public Category GetById(long id) => _context.Categories.Find(id);

        public Category GetById(long id, string includeProperty = null)
        {
            IQueryable<Category> query = _context.Categories;
            query = query.Where(x => x.Id == id).Include(includeProperty);

            return query.ToList().FirstOrDefault();
        }
        #endregion

        #region Operations
        public void Add(Category entity)
        {
            if (entity != null)
                _context.Categories.Add(entity);
        }
        public void Remove(Category entity) => _context.Categories.Remove(entity);

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
        #endregion

        #region Others
        public int GetCount(Expression<Func<Category, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Category> query = _context.Categories;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }
        #endregion
    }
}
