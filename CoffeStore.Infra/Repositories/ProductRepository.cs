using CoffeStore.Infra.Context;
using CoffeStore.Infra.Settings;
using CoffeStore.Models.Aggregates.ProductAggregate;
using CoffeStore.Models.Contracts.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepository(CoffeStoreDbContext context)
        {           
            _productsCollection = context.Products;
        }

        public async Task AddAsync(Product product)
        {            
            await _productsCollection.InsertOneAsync(product);
        }

        public async Task<ICollection<Product>> GetAvailableProductsAsync()
        {            
            return await GetAvailableSortedByDescending().ToListAsync();
        }

        public string[] GetFeaturedProductsImages()
        {           
             var featureProducts =  GetAvailableSortedByDescending()
                                    .Limit(4)
                                    .ToList();

            return featureProducts.Select(p => p.ImagePath).ToArray();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        private IOrderedFindFluent<Product, Product> GetAvailableSortedByDescending()
        {
            return _productsCollection.Find(p => p.IsAvailable)
                                        .SortByDescending(p => p.CreatedAt);
        }
    }
}
