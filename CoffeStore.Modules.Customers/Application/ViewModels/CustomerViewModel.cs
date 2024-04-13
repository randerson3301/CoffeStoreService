using CoffeStore.Common.Seedwork;

namespace CoffeStore.Modules.Customers.Application.ViewModels
{
    internal class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public IEnumerable<DeliveryAddress> Addresses { get; set; }
    }
}
