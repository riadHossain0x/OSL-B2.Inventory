using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Repository.DbContexts;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace OSL_B2.Inventory.Repository
{
    public interface IPurchaseRepository
    {
        #region Load instances
        (IList<Purchase> data, int total, int totalDisplay) LoadAll(Expression<Func<Purchase, bool>> datefilter = null, Expression<Func<Purchase, bool>> filter = null,
            string includeProperties = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null);
        #endregion

        #region Operations
        void Add(Purchase entity);
        void Edit(Purchase entity);
        void Remove(Purchase entity);
        void SaveChanages();
        Task SaveChanagesAsync();
        #endregion

        #region Single instances
        Purchase GetById(long id);
        Purchase GetById(long id, string includeProperty);
        #endregion

        #region Others
        int GetCount(Expression<Func<Purchase, bool>> filter = null);
        #endregion
    }

    public class PurchaseRepository : IPurchaseRepository
    {
        #region Initialization
        private readonly IMSDbContext _context;

        public PurchaseRepository(IMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Load instances
        public (IList<Purchase> data, int total, int totalDisplay) LoadAll(Expression<Func<Purchase, bool>> datefilter = null, Expression<Func<Purchase, bool>> filter = null,
            string includeProperties = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null)
        {
            IQueryable<Purchase> query = _context.Purchases;
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
                case "Purchase No.":
                    query = sortDir == "asc" ? query.OrderBy(c => c.PurchaseNo) : query.OrderByDescending(c => c.PurchaseNo);
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

        #region Operations
        public void Add(Purchase entity)
        {
            if (entity != null)
                _context.Purchases.Add(entity);
        }

        public void Edit(Purchase entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Purchases.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(Purchase entity) => _context.Purchases.Remove(entity);

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
        #endregion

        #region Single instances
        public Purchase GetById(long id)
        {
            IQueryable<Purchase> query = _context.Purchases;
            query = query.Where(x => x.Id == id && x.IsActive != Status.Inactive);

            return query.ToList().FirstOrDefault();
        }

        public Purchase GetById(long id, string includeProperty)
        {
            IQueryable<Purchase> query = _context.Purchases;
            query = query.Where(x => x.Id == id && x.IsActive != Status.Inactive).Include(includeProperty);

            return query.ToList().FirstOrDefault();
        }
        #endregion

        #region Others
        public int GetCount(Expression<Func<Purchase, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Purchase> query = _context.Purchases;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }
        #endregion
    }
}
