using CoffeStore.Modules.Customers.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.Queries
{
    public class GetCustomerQuery: IRequest<CustomerViewModel>
    {
        public Guid Id { get; set; }
    }
}
