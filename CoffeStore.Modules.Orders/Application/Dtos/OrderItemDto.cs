namespace CoffeStore.Modules.Orders.Application.Dtos
{
    internal class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public uint Quantity { get; set; }
    }
}
