using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Commands
{
    internal class CreateProductCommand : IRequest<ProductViewModel?>
    {
        public required string Name { get; set; }
        public required string ImagePath { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
    }
}
