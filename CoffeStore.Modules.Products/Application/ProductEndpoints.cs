using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Products.Application.Commands;
using CoffeStore.Modules.Products.Application.Queries;
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

            app.MapGet("/product/{id}",
                async (Guid id, IMediator mediator, IErrorContext errorContext) =>
                {
                    var command = new GetProductByIdQuery(id);
                    var result = await mediator.Send(command);

                    if (result == null) return Results.NotFound(errorContext.GetErrors());

                    return Results.Ok(result);
                }).WithOpenApi()
                .Produces((int)HttpStatusCode.OK)
                .Produces((int)HttpStatusCode.NotFound);

            app.MapGet("/product/",
                async ([FromQuery] bool onlyAvailable, IMediator mediator, IErrorContext errorContext) =>
                {
                    var command = new GetProductsByFiltersQuery(onlyAvailable);
                    var result = await mediator.Send(command);

                    return Results.Ok(result);
                }).WithOpenApi()
                .Produces((int)HttpStatusCode.OK);
        }
    }
}
