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
    public interface ISupplierRepository
    {
        #region Load instances
        (IList<Supplier> data, int total, int totalDisplay) LoadAll(Expression<Func<Supplier, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null);
        #endregion

        #region Single instances
        Supplier GetById(long id);
        #endregion

        #region Operations
        void Add(Supplier entity);
        void Edit(Supplier entity);
        void Remove(Supplier entity);
        void SaveChanages();
        Task SaveChanagesAsync();
        #endregion

        #region Others
        int GetCount(Expression<Func<Supplier, bool>> filter = null);
        #endregion
    }

    public class SupplierRepository : ISupplierRepository
    {
        #region Initialization
        private readonly IMSDbContext _context;

        public SupplierRepository(IMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Load instances
        public (IList<Supplier> data, int total, int totalDisplay) LoadAll(Expression<Func<Supplier, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null)
        {
            IQueryable<Supplier> query = _context.Suppliers;
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
                case "Phone":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Mobile) : query.OrderByDescending(c => c.Mobile);
                    break;
                case "Address":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Address) : query.OrderByDescending(c => c.Address);
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

        public Supplier GetById(long id)
        {
            IQueryable<Supplier> query = _context.Suppliers;
            query = query.Where(x => x.Id == id && x.IsActive != Status.Inactive);

            return query.ToList().FirstOrDefault();
        }
        #endregion

        #region Operations
        public void Add(Supplier entity)
        {
            if (entity != null)
                _context.Suppliers.Add(entity);
        }

        public void Edit(Supplier entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Suppliers.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(Supplier entity) => _context.Suppliers.Remove(entity);

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
        #endregion

        #region Others
        public int GetCount(Expression<Func<Supplier, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Supplier> query = _context.Suppliers;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }
        #endregion
    }
}
