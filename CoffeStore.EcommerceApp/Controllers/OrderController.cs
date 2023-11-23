using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Adapters.Contracts;
using CoffeStore.EcommerceApp.Requests.Order;
using CoffeStore.Models.Aggregates.OrderAggregate;
using CoffeStore.Models.Contracts.Repositories;
using CoffeStore.Models.Enums;
using CoffeStore.Models.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeStore.EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderAdapter _adapter;
        private readonly ILogger<CustomerController> _logger;

        public OrderController(IOrderRepository repository, IOrderAdapter adapter, ILogger<CustomerController> logger)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderRequest request)
        {
            Order order = _adapter.ConvertToDomain(request);

            try
            {
                await _repository.AddAsync(order);
                return Ok(order);
            } catch (Exception error)
            {
                _logger.LogError(error, "Adding new order has failed");
                return BadRequest("Adding new order has failed");
            }
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetOrdersByCustomerId (string id)
        {
            try
            {
                return Ok(await _repository.GetOrderByCustomerAsync(id));
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error on getting customer orders");

                return BadRequest("Error on getting customer orders");
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, DeliveryStatusEnum deliveryStatus)
        {
            try
            {
                Order order = await _repository.GetOrderByIdAsync(id);

                order.ChangeDeliveryStatus(deliveryStatus);

                await _repository.UpdateOrderStatusAsync(id, order);
                return NoContent();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error on updating order");

                return BadRequest("Error on updating order");
            }
        }

    }
}
