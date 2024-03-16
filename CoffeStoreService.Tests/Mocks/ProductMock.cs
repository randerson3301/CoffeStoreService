using CoffeStore.Modules.Products.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Tests.Mocks
{
    internal static class ProductMock
    {
        public static CreateProductCommand GetCommand()
        {
            return new CreateProductCommand()
            {
                Name = "Qualquer um",
                ImagePath = "<<imagem>>",
                Price = 10.65m,
                Description = "cafezin bão"
            };
        }
    }
}
