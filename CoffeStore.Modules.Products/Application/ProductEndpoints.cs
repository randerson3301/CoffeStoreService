using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Products.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoffeStore.Modules.Products.Application
{
    public static class ProductEndpoints
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/product",
                async ([FromBody] CreateProductCommand product, IMediator mediator, IErrorContext errorContext) =>
                {
                    var result = await mediator.Send(product);

                    if (result == null) return Results.BadRequest(errorContext.GetErrors());

                    return Results.Created("/product", result);
                }).WithOpenApi()
                .Produces((int)HttpStatusCode.Created)
                .Produces((int)HttpStatusCode.BadRequest);
        }
    }
}
