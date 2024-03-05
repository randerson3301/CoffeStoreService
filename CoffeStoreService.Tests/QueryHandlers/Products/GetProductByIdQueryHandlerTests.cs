using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Customers.Application.Queries;
using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.Queries;
using CoffeStore.Modules.Products.Application.Queries.Handlers;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using CoffeStoreService.Tests.Mocks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Tests.QueryHandlers.Products
{
    internal class GetProductByIdQueryHandlerTests
    {
        private IProductRepository _repository;
        private IProductAdapter _adapter;
        private IErrorContext _errorContext;

        private GetProductByIdQueryHandler _queryHandler;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IProductRepository>();
            _adapter = Substitute.For<IProductAdapter>();
            _errorContext = new ErrorContext();
            _queryHandler = new GetProductByIdQueryHandler(_repository, _adapter, _errorContext);
        }

        [Test]
        public async Task Get_Product_Returns_Success()
        {
            var product = new Product("dsada", "sdasd", 18.85m, "aassad", Guid.NewGuid());

            var productQuery = new GetProductByIdQuery(Guid.NewGuid());           

            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(product);
            _adapter.ConvertToViewModel(product).Returns(new ProductViewModel());

            var result = await _queryHandler.Handle(productQuery, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProductViewModel>(result);
        }

        [Test]
        public async Task Get_Product_Returns_NotFound()
        {
            var productQuery = new GetProductByIdQuery(Guid.NewGuid());

            _repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var result = await _queryHandler.Handle(productQuery, new CancellationToken());

            Assert.IsNull(result);
            Assert.IsNotNull(_errorContext.GetErrors());
            Assert.AreEqual(ErrorType.NotFound, _errorContext.GetErrors().ErrorType);
        }
    }
}
