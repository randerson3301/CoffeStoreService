using CoffeStore.Modules.Products.Application.Queries;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoffeStore.Modules.Products.Infra.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private ProductContext db;

        public ProductRepository(ProductContext db)
        {
            this.db = db;
        }

        public async Task<Product> AddAsync(Product data)
        {
            await db.Products.AddAsync(data);
            await db.SaveChangesAsync();
            return data;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await db.Products.Where(c => c.Id == id)
                             .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Product>> GetProductsByFiltersAsync(GetProductsByFiltersQuery query)
        {
            var filters = new List<Func<Product, bool>>();

            if (query.OnlyAvailable)
            {
                filters.Add(p => p.IsAvailable);
            }            

            var products = new List<Product>();

            foreach (var filter in filters)
            {
                products.AddRange(db.Products.Where(filter));
            }

            if (!filters.Any())
            {
                products = await db.Products.ToListAsync();
            }

            return products;
        }

        public async Task UpdateAsync(Product data)
        {
            db.Update(data);
            db.Entry(data).State = EntityState.Modified;
            await db.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
