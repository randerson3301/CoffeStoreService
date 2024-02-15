using CoffeStore.Modules.Customers;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStore.Modules.Customers.Infra.PersistenceModels;
using CoffeStore.Modules.Customers.Seedwork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public Task<Customer> GetByEmail(string email)
        {
            throw new NotImplementedException();
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
