using CoffeStore.Modules.Orders.Application.ExternalServices.Contracts;
using CoffeStore.Modules.Customers.Application;

namespace CoffeStore.Modules.Orders.Application.ExternalServices
{
    public class CustomerExternalService : ICustomerExternalService
    {
        private ICustomerApiService _apiService;

        public CustomerExternalService(ICustomerApiService apiService)
        {
            _apiService = apiService;
        }

        public bool CustomerExists(Guid customerId)
        {
            return _apiService.GetCustomerById(customerId) != Results.NotFound();
        }
    }
}
