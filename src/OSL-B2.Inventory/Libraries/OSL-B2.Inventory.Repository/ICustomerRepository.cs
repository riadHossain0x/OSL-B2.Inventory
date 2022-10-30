﻿using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
}
