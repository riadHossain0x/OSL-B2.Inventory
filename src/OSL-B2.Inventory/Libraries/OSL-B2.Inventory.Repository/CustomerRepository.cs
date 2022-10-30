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

        public IList<Customer> GetAll()
        {
            IQueryable<Customer> query = _context.Customers;
            query = query.Where(x => x.IsActive == Status.Active);
            return query.ToList();
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
