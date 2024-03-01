using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductViewModel?>
    {
        public string Name { get; internal set; }
        public string ImagePath { get; internal set; }
        public decimal Price { get; internal set; }
        public string Description { get; internal set; }
    }
}
