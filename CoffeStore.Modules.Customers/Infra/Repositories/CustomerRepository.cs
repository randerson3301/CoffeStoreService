﻿using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CoffeStore.Modules.Customers.Infra.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private CustomerContext db;

        public CustomerRepository(CustomerContext db)
        {
            this.db = db;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await db.Customers.AddAsync(customer);
            await db.SaveChangesAsync();
            return customer;
        }                

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await db.Customers.Where(c => c.Id == id)
                 .FirstOrDefaultAsync();
        }        

        public async Task UpdateAsync(Customer customer)
        {
            db.Update(customer);
            db.Entry(customer).State = EntityState.Modified;
            await db.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
