using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeStore.Modules.Products.Application.Commands;
using CoffeStore.Modules.Products.Application.Commands.Handlers;

namespace CoffeStore.Tests.CommandHandlers.Products
{
    internal class CreateProductCommandHandlerTests
    {
        private CreateProductCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _handler = new CreateProductCommandHandler();
        }

        [Test]
        public async void Add_NewProduct_Returns_Success()
        {
            var request = new CreateProductCommand()
            {
                Name = "Qualquer um",
                ImagePath = "",
                Price = 10.65m,
                Description = "cafezin bão"
            };

            await _handler.Handle(request, new CancellationToken());
        }
    }
}
