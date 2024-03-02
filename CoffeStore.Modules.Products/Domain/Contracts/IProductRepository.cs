using CoffeStore.Common.Data;

namespace CoffeStore.Modules.Products.Domain.Contracts
{
    internal interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetAvailableProductsAsync();
    }
}
