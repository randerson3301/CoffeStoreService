using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.Models.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        Task Update(Customer customer);
    }
}