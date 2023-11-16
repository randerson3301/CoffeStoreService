using CoffeStore.Models.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Models.Contracts.Repositories
{
    public interface IProductRepository
    {
        string[] GetFeaturedProductsImages();
        Task AddAsync(Product product);
        Task<ICollection<Product>> GetAvailableProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
    }
}
