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
        (IList<Customer> data, int total, int totalDisplay) GetAll(Expression<Func<Customer, bool>> filter = null,
            string orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10);
        Customer GetById(long id);
        Customer GetById(long id, string includeProperties = null);
        int GetCount(Expression<Func<Customer, bool>> filter = null);
        void Add(Customer entity);
        void Edit(Customer entity);
        void Remove(Customer entity);
        void SaveChanages();
        Task SaveChanagesAsync();
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMSDbContext _context;

        public CustomerRepository(IMSDbContext context)
        {
            _context = context;
        }

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

        public (IList<Customer> data, int total, int totalDisplay) GetAll(Expression<Func<Customer, bool>> filter = null, 
            string orderBy = null, string includeProperties = "", int pageIndex = 1 , int pageSize = 10)
        {
            IQueryable<Customer> query = _context.Customers;
            var total = query.Count();
            var totalDisplay = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Where(x => x.IsActive == Status.Active);

            totalDisplay = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            var result = query.OrderBy(x => x.Name).Skip(pageIndex).Take(pageSize);

            return (result.ToList(), total, totalDisplay);
        }

        public Customer GetById(long id) => _context.Customers.Find(id);

        public Customer GetById(long id, string includeProperties = null)
        {
            IQueryable<Customer> query = _context.Customers;
            query = query.Where(x => x.Id == id);

            foreach (var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList().FirstOrDefault();
        }

        public int GetCount(Expression<Func<Customer, bool>> filter = null)
        {
            var count = 0;

            IQueryable<Customer> query = _context.Customers;

            if (filter != null)
                query = query.Where(filter);

            count = query.Count();

            return count;
        }

        public void Remove(Customer entity) => _context.Customers.Remove(entity);

        public void SaveChanages() => _context.SaveChanges();

        public async Task SaveChanagesAsync() => await _context.SaveChangesAsync();
    }
}
