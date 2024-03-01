using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductViewModel?>
    {
        public Task<ProductViewModel?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
