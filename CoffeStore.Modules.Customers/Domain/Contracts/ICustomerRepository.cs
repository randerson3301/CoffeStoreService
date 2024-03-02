using CoffeStore.Common.Data;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Seedwork;

namespace CoffeStore.Modules.Customers.Domain.Contracts
{
    internal interface ICustomerRepository: IRepository<Customer>
    {
        
    }
}