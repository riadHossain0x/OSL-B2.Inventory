using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository
{
    public interface ISaleRepository
    {
        #region Load instances
        (IList<Sale> data, int total, int totalDisplay) LoadAll(Expression<Func<Sale, bool>> datefilter = null, Expression<Func<Sale, bool>> filter = null,
            string includeProperties = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null);
        #endregion

        #region Operations
        void Add(Sale entity);
        void Edit(Sale entity);
        void Remove(Sale entity);
        void SaveChanages();
        Task SaveChanagesAsync();
        #endregion

        #region Single instances
        Sale GetById(long id);
        Sale GetById(long id, string includeProperty);
        #endregion

        #region Others
        int GetCount(Expression<Func<Sale, bool>> filter = null);
        #endregion
    }
    public class SaleRepository : ISaleRepository
    {
        #region Initialization
        private readonly IMSDbContext _context;

        public SaleRepository(IMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Load instances
        public (IList<Sale> data, int total, int totalDisplay) LoadAll(Expression<Func<Sale, bool>> datefilter = null, Expression<Func<Sale, bool>> filter = null,
            string includeProperties = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null)
        {
            IQueryable<Sale> query = _context.Sales;
            query = query.Where(x => x.IsActive == Status.Active);

            var total = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (datefilter != null)
            {
                query = query.Where(datefilter);
            }

            var totalDisplay = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            //sorting
            switch (sortBy)
            {
                case "Customer Name":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Customer.Name) : query.OrderByDescending(c => c.Customer.Name);
                    break;
                case "Total Amount":
                    query = sortDir == "asc" ? query.OrderBy(c => c.GrandTotal) : query.OrderByDescending(c => c.GrandTotal);
                    break;
                case "Discount Amount":
                    query = sortDir == "asc" ? query.OrderBy(c => c.DiscountTotal) : query.OrderByDescending(c => c.DiscountTotal);
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

        #region Operations
        public void Add(Sale entity)
        {
            if (entity != null)
                _context.Sales.Add(entity);
        }

        public void Edit(Sale entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Sales.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(Sale entity) => _context.Sales.Remove(entity);

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
        #endregion

        #region Single instances
        public Sale GetById(long id)
        {
            IQueryable<Sale> query = _context.Sales;
            query = query.Where(x => x.Id == id && x.IsActive != Status.Inactive);

            return query.ToList().FirstOrDefault();
        }

        public Sale GetById(long id, string includeProperty)
        {
            IQueryable<Sale> query = _context.Sales;
            query = query.Where(x => x.Id == id && x.IsActive != Status.Inactive).Include(includeProperty);

            return query.ToList().FirstOrDefault();
        }
        #endregion

        #region Others
        public int GetCount(Expression<Func<Sale, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Sale> query = _context.Sales;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }
        #endregion
    }
}
