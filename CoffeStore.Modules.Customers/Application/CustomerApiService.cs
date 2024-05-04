using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Queries;
using MediatR;

namespace CoffeStore.Modules.Customers.Application
{
    internal class CustomerApiService : ICustomerApiService
    {
        private IMediator _mediator;
        private IErrorContext _errorContext;

        public CustomerApiService(IMediator mediator, IErrorContext errorContext)
        {
            _mediator = mediator;
            _errorContext = errorContext;
        }

        public async Task<IResult> AddCustomer(CreateCustomerCommand newCustomer)
        {
            var result = await _mediator.Send(newCustomer);

            if (result == null)
            {
                return Results.BadRequest(_errorContext.GetErrors());
            }

            return Results.Created("/customer", result);
        }        

        public async Task<IResult> AddCustomerAddress(Guid id, CreateCustomerAddressCommand newAddress)
        {
            newAddress.Id = id;
            var result = await _mediator.Send(newAddress);

            if (result == null)
            {
                var error = _errorContext.GetErrors();

                if (error.ErrorType == ErrorType.NotFound)
                {
                    return Results.NotFound(error);
                }

                return Results.BadRequest(error);
            }

            return Results.Ok(result);
        }

        public async Task<IResult> RemoveCustomerAddress(Guid id, DeleteCustomerAddressCommand addressToRemove)
        {
            addressToRemove.Id = id;
            var result = await _mediator.Send(addressToRemove);

            if (!result)
            {
                var error = _errorContext.GetErrors();

                if (error.ErrorType == ErrorType.NotFound)
                {
                    return Results.NotFound(error);
                }

                return Results.BadRequest(error);
            }

            return Results.NoContent();
        }

        public async Task<IResult> GetCustomerById(Guid id)
        {
            var query = new GetCustomerQuery() { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                var error = _errorContext.GetErrors();

                if (error.ErrorType == ErrorType.NotFound)
                {
                    return Results.NotFound(error);
                }
            }

            return Results.Ok(result);
        }
    }
}
