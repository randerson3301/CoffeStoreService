namespace CoffeStore.Modules.Orders.Application.ExternalServices.Contracts
{
    internal interface ICustomerExternalService
    {
        public bool CustomerExists(Guid customerId);
    }
}
