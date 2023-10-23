using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.Models.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> GetByIdAsync(Guid? id);
        Task UpdateAsync(Customer customer);
    }
}