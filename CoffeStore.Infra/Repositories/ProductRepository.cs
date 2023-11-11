using CoffeStore.Infra.Context;
using CoffeStore.Infra.Settings;
using CoffeStore.Models.Aggregates.ProductAggregate;
using CoffeStore.Models.Contracts.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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

        public string[] GetFeaturedProductsImages()
        {           
             var featureProducts = _productsCollection.Find(p => p.IsAvailable)
                                    .SortByDescending(p => p.CreatedAt)
                                    .Limit(4)
                                    .ToList();

            return featureProducts.Select(p => p.ImagePath).ToArray();
        }
    }
}
