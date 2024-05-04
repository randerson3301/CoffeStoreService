using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoffeStore.Modules.Customers.Application.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void Map(WebApplication app)
        {            
            app.MapPost("/customer",
              async ([FromBody] CreateCustomerCommand newCustomer, ICustomerApiService apiService) =>
              {   
                  
                  return await apiService.AddCustomer(newCustomer);

              }).WithOpenApi()
              .Produces((int)HttpStatusCode.Created)
              .Produces((int)HttpStatusCode.BadRequest);


            app.MapPost("/customer/{id}/address",
                async (Guid id, [FromBody] CreateCustomerAddressCommand newAddress, ICustomerApiService apiService) =>
                {

                    return await apiService.AddCustomerAddress(id, newAddress);

                }).WithOpenApi()
                .Produces((int)HttpStatusCode.OK)
                .Produces((int)HttpStatusCode.NotFound)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapDelete("/customer/{id}/address",
                async (Guid id, [FromBody] DeleteCustomerAddressCommand addressToRemove, ICustomerApiService apiService) =>
                {

                    return await apiService.RemoveCustomerAddress(id, addressToRemove);

                }).WithOpenApi()
                .Produces((int)HttpStatusCode.NoContent)
                .Produces((int)HttpStatusCode.NotFound)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapGet("/customer/{id}", async (Guid id, ICustomerApiService apiService) =>
            {
                
                return await apiService.GetCustomerById(id);

            }).WithOpenApi()
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound);
        }
    }
}
