using CoffeStore.Models.Aggregates.OrderAggregate;
using CoffeStore.Models.Aggregates.ProductAggregate;
using CoffeStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Models.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<ICollection<Order>> GetOrderByCustomerAsync(string customerId);
        Task UpdateOrderStatusAsync(string ordersId, Order order);
        Task<Order> GetOrderByIdAsync(string id);
    }
}
