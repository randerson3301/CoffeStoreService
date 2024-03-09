using CoffeStore.Modules.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeStore.Modules.Products.Infra
{
    internal class ProductContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        public ProductContext(DbContextOptions options): base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        }

        private class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.ProductName).IsRequired();
                builder.Property(p => p.Price).HasColumnType("decimal").IsRequired();
                builder.Property(p => p.ImagePath).IsRequired();
                builder.Property(p => p.Description).IsRequired();
                builder.Property(p => p.AddedBy).IsRequired();
                builder.Property(p => p.CreatedAt).IsRequired();                

                builder.OwnsMany<ProductReview>("_productReviews", pr =>
                {
                    pr.ToTable("ProductReviews");
                    pr.HasKey(pr => new { pr.ProductId, pr.CustomerId });
                    pr.WithOwner().HasForeignKey(pr => pr.ProductId);
                    pr.Property(pr => pr.RateNumber).IsRequired();
                    pr.Property(pr => pr.Comment);
                    pr.Property(pr => pr.CustomerId).IsRequired();
                });               
            }
        }
    }
}
