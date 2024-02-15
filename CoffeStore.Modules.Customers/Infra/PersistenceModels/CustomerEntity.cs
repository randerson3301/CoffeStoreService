using CoffeStore.Modules.Customers.Domain;

namespace CoffeStore.Modules.Customers.Infra.PersistenceModels
{
    internal class CustomerEntity
    {
        public void Set(Customer customer)
        {
            Document = customer.Document;
            Email = customer.Email;
            FullName = customer.FullName;
            BirthDate = customer.BirthDate;
            Id = customer.Id;
        }

        public Guid Id { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Document { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CustomerAddressEntity>? Addresses { get; set; }
    }
}
