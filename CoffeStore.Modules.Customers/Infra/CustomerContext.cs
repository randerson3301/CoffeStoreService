using CoffeStore.Modules.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeStore.Modules.Customers.Infra
{
    internal class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerDeliveryAddresses { get; set; }        

        public CustomerContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
        }

        private class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
        {
            public void Configure(EntityTypeBuilder<Customer> builder)
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.BirthDate).IsRequired();
                builder.Property(c => c.Document).IsRequired();
                builder.Property(c => c.FullName).IsRequired();
                builder.Property(c => c.Email).IsRequired();

                builder.OwnsMany<CustomerAddress>("_deliveryAddresses", ca =>
                {
                    ca.ToTable("CustomerDeliveryAddresses");
                    ca.HasKey(ca => new { ca.CustomerId, ca.ZipCode, ca.Number });
                    ca.WithOwner().HasForeignKey(ca => ca.CustomerId);
                    ca.Property(ca => ca.Address).HasColumnName("Address");
                    ca.Property(ca => ca.City).HasColumnName("City");
                    ca.Property(ca => ca.Complement).HasColumnName("Complement");
                    ca.Property(ca => ca.Neighborhood).HasColumnName("Neighborhood");
                    ca.Property(ca => ca.Number).HasColumnName("Number");
                    ca.Property(ca => ca.State).HasColumnName("State");
                    ca.Property(ca => ca.ZipCode).HasColumnName("ZipCode");
                });
            }
        }       
    }
}
