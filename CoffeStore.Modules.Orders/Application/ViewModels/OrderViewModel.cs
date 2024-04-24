
using CoffeStore.Modules.Orders.Domain.Enums;

namespace CoffeStore.Modules.Orders.Application.ViewModels
{
    internal class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DeliveryStatusEnum DeliveryStatus { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }

    }
}
