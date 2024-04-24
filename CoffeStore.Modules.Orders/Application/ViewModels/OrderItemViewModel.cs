namespace CoffeStore.Modules.Orders.Application.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public short RatingNumber { get; set; }
    }
}