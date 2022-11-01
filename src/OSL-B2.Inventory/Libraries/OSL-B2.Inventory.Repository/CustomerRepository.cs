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
    public interface ICustomerRepository
    {
        #region Load instances
        (IList<Customer> data, int total, int totalDisplay) LoadAll(Expression<Func<Customer, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null);
        #endregion

        #region Single instances
        Customer GetById(long id);
        Customer GetById(long id, string includeProperties = null);
        #endregion

        #region Operations
        void Add(Customer entity);
        void Edit(Customer entity);
        void Remove(Customer entity);
        void SaveChanages();
        Task SaveChanagesAsync();
        #endregion

        #region Others
        int GetCount(Expression<Func<Customer, bool>> filter = null);
        #endregion

    }

    public class CustomerRepository : ICustomerRepository
    {
        #region Initialization
        private readonly IMSDbContext _context;

        public CustomerRepository(IMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Load instances
        public (IList<Customer> data, int total, int totalDisplay) LoadAll(Expression<Func<Customer, bool>> filter = null,
            string orderBy = null, int pageIndex = 1, int pageSize = 10, string sortBy = null, string sortDir = null)
        {
            IQueryable<Customer> query = _context.Customers;
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
                case "Email":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Email) : query.OrderByDescending(c => c.Email);
                    break;
                case "Phone":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Mobile) : query.OrderByDescending(c => c.Mobile);
                    break;
                case "Address":
                    query = sortDir == "asc" ? query.OrderBy(c => c.Address) : query.OrderByDescending(c => c.Address);
                    break;
            }

            var result = query.OrderBy(x => x.Name).Skip(pageIndex).Take(pageSize);

            return (result.ToList(), total, totalDisplay);
        }
        #endregion

        #region Single instances
        public Customer GetById(long id) => _context.Customers.Find(id);

        public Customer GetById(long id, string includeProperties = null)
        {
            IQueryable<Customer> query = _context.Customers;
            query = query.Where(x => x.Id == id);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList().FirstOrDefault();
        }
        #endregion

        #region Operations
        public void Add(Customer entity)
        {
            if (entity != null)
                _context.Customers.Add(entity);
        }

        public void Edit(Customer entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Customers.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(Customer entity) => _context.Customers.Remove(entity);

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
        #endregion

        #region Others
        public int GetCount(Expression<Func<Customer, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Customer> query = _context.Customers;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }
        #endregion
    }
}
