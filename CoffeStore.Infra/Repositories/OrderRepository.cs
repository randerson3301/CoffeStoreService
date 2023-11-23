using CoffeStore.Infra.Context;
using CoffeStore.Models.Aggregates.CustomerAggregate;
using CoffeStore.Models.Aggregates.OrderAggregate;
using CoffeStore.Models.Aggregates.ProductAggregate;
using CoffeStore.Models.Contracts.Repositories;
using CoffeStore.Models.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderRepository(CoffeStoreDbContext context)
        {
            _orderCollection = context.Orders;
        }

        public async Task AddAsync(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
        }

        public async Task<ICollection<Order>> GetOrderByCustomerAsync(string customerId)
        {
            return await _orderCollection.Find(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task UpdateOrderStatusAsync(string orderId, Order order)
        {
            await _orderCollection.ReplaceOneAsync(o => o.Id == orderId, order);
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            return await _orderCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }
    }
}
