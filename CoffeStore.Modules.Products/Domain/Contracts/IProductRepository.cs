using CoffeStore.Common.Data;
using CoffeStore.Modules.Products.Application.Queries;

namespace CoffeStore.Modules.Products.Domain.Contracts
{
    internal interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetProductsByFiltersAsync(GetProductsByFiltersQuery query);
    }
}
