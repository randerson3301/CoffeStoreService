using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.Models.Aggregates.CustomerAggregate;
using CoffeStore.Models.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace CoffeStore.EcommerceApp.Controllers
{

    public class CustomerController
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository repository, ICustomerAdapter adapter, ILogger<CustomerController> logger)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
        }

        public async Task<Customer> Add(CustomerDto dto)
        {
            //validates dto with fluent validation

            var domain = _adapter.ConvertToDomain(dto);
            
            try
            {
                domain = await _repository.Add(domain);
                return domain;

            } catch(Exception)
            {
                throw;
            }
        }
    }
}