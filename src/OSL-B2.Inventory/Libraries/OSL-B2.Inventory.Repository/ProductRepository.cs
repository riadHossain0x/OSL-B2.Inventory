using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository
{
    public interface IProductRepository
    {
        #region Load instances
        (IList<Product> data, int total, int totalDisplay) LoadAll(Expression<Func<Product, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null);
        #endregion

        #region Single instances
        Product GetById(long id);
        #endregion

        #region Operations
        void Add(Product entity);
        void Edit(Product entity);
        void Remove(Product entity);
        void SaveChanages();
        Task SaveChanagesAsync();
        #endregion

        #region Others
        int GetCount(Expression<Func<Product, bool>> filter = null);
        #endregion
    }
    public class ProductRepository : IProductRepository
    {
        #region Initialization
        private readonly IMSDbContext _context;

        public ProductRepository(IMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Load instances
        public (IList<Product> data, int total, int totalDisplay) LoadAll(Expression<Func<Product, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null)
        {
            IQueryable<Product> query = _context.Products;
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
                case "Category":
                    query = sortBy == "asc" ? query.OrderBy(c => c.Category.Name) : query.OrderByDescending(c => c.Category.Name);
                    break;
                case "Details":
                    query = sortBy == "asc" ? query.OrderBy(c => c.Details) : query.OrderByDescending(c => c.Details);
                    break;
                case "Created By":
                    query = sortDir == "asc" ? query.OrderBy(c => c.CreatedBy) : query.OrderByDescending(c => c.CreatedBy);
                    break;
                case "Modified By":
                    query = sortDir == "asc" ? query.OrderBy(c => c.ModifiedBy) : query.OrderByDescending(c => c.ModifiedBy);
                    break;
            }

            var result = query.Skip(pageIndex).Take(pageSize);

            return (result.ToList(), total, totalDisplay);
        }
        #endregion

        #region Single instances
        public Product GetById(long id)
        {
            IQueryable<Product> query = _context.Products;
            query = query.Where(x => x.Id == id && x.IsActive != Status.Inactive);

            return query.ToList().FirstOrDefault();
        }
        #endregion

        #region Operations
        public void Add(Product entity)
        {
            if (entity != null)
                _context.Products.Add(entity);
        }

        public void Edit(Product entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Products.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(Product entity) => _context.Products.Remove(entity);

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
        #endregion

        #region Others
        public int GetCount(Expression<Func<Product, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Product> query = _context.Products;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }
        #endregion
    }
}
