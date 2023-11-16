using CoffeStore.Infra.Context;
using CoffeStore.Models.Aggregates.CustomerAggregate;
using CoffeStore.Models.Aggregates.ProductAggregate;
using CoffeStore.Models.Contracts.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(CoffeStoreDbContext context)
        {
            _customerCollection = context.Customers;
        }

        public async Task AddAsync(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await _customerCollection.Find(c => c.CustomerAccess.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customerCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task<bool> IsCustomerRegistered(string email, string? password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
