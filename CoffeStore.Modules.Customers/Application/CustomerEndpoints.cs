using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Queries;
using CoffeStore.Modules.Customers.Seedwork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoffeStore.Modules.Customers.Application
{
    public static class CustomerEndpoints
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/customer",
                async ([FromBody] CreateCustomerCommand newCustomer, IMediator mediator, IErrorContext errorContext) =>
                {
                    var result = await mediator.Send(newCustomer);

                    if (result == null) return Results.BadRequest(errorContext.GetErrors());

                    return Results.Created("/customer", result);
                }).WithOpenApi()
                .Produces((int)HttpStatusCode.Created)
                .Produces((int)HttpStatusCode.BadRequest);


            app.MapPost("/customer/{id}/address",
                async (Guid id, [FromBody] CreateCustomerAddressCommand newAddress, IMediator mediator, IErrorContext errorContext) =>
                {
                    newAddress.Id = id;
                    var result = await mediator.Send(newAddress);

                    if (result == null)
                    {
                        var error = errorContext.GetErrors();

                        if (error.ErrorType == ErrorType.NotFound)
                        {
                            return Results.NotFound(error);
                        }

                        return Results.BadRequest(error);
                    }

                    return Results.Ok(result);
                }).WithOpenApi()
                .Produces((int)HttpStatusCode.OK)
                .Produces((int)HttpStatusCode.NotFound)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapDelete("/customer/{id}/address",
                async (Guid id, [FromBody] DeleteCustomerAddressCommand addressToRemove, IMediator mediator, IErrorContext errorContext) =>
                {
                    addressToRemove.Id = id;
                    var result = await mediator.Send(addressToRemove);

                    if (!result)
                    {
                        var error = errorContext.GetErrors();

                        if (error.ErrorType == ErrorType.NotFound)
                        {
                            return Results.NotFound(error);
                        }

                        return Results.BadRequest(error);
                    }

                    return Results.NoContent();

                }).WithOpenApi()
                .Produces((int)HttpStatusCode.NoContent)
                .Produces((int)HttpStatusCode.NotFound)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapGet("/customer/{id}", async (Guid id, IMediator mediator, IErrorContext errorContext) =>
            {
                var query = new GetCustomerQuery() { Id = id };
                var result = await mediator.Send(query);

                if (result == null)
                {
                    var error = errorContext.GetErrors();

                    if (error.ErrorType == ErrorType.NotFound)
                    {
                        return Results.NotFound(error);
                    }
                }

                return Results.Ok(result);
            }).WithOpenApi()
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound);
        }
    }
}
