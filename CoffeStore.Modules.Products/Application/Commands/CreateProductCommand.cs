using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Commands
{
    internal class CreateProductCommand : IRequest<ProductViewModel?>
    {
        public required string Name { get; internal set; }
        public required string ImagePath { get; internal set; }
        public decimal Price { get; internal set; }
        public required string Description { get; internal set; }
    }
}
