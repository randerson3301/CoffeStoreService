using CoffeStore.Modules.Products.Application.Queries;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

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

        public Task<ICollection<Product>> GetProductsByFiltersAsync(GetProductsByFiltersQuery query)
        {
            throw new NotImplementedException();
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
