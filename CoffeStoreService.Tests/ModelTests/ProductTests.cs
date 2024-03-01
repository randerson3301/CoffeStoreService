using CoffeStore.Modules.Products.Domain;
using NUnit.Framework;

namespace CoffeStoreService.Tests.ModelTests
{
    internal class ProductTests
    {
        [Test]
        public void RateProduct_ProductAverageRateNumber_EqualsRateNumber()
        {
            var product = new Product("produto", "path", 8.5m, "test", Guid.NewGuid());

            int rateNumber = 5;

            var rate = new ProductReview(Guid.NewGuid(), "muito top", rateNumber);

            product.AddReview(rate);

            Assert.AreEqual(rateNumber, product.AverageRate);
        }

        [Test]
        public void RateProduct_ProductRateNumber_EqualsAverageProductRateNumber()
        {
            var product = new Product("produto", "path", 8.5m, "test", Guid.NewGuid());

            int rateNumber = 5;
            int rateNumber2 = 8;

            var rate = new ProductReview(Guid.NewGuid(), "muito top", rateNumber);
            var rate2 = new ProductReview(Guid.NewGuid(), "muito top", rateNumber2);

            product.AddReview(rate);
            product.AddReview(rate2);

            var averageRate = (double)(rateNumber + rateNumber2) / 2;

            Assert.AreEqual(averageRate, product.AverageRate);
        }

        [Test]
        public void Disable_Product_TurnsProductUnavailable()
        {
            var product = new Product("produto", "path", 8.5m, "test", Guid.NewGuid());

            product.Disable();

            Assert.IsFalse(product.IsAvailable);
        }       

        [Test]
        public void Enable_DisabledProduct_TurnsProductAvailableAgain()
        {
            var product = new Product("produto", "path", 8.5m, "test", Guid.NewGuid());

            product.Disable();
            product.Enable();

            Assert.IsTrue(product.IsAvailable);
        }

        [Test]
        public void Remove_ProductReview_ChangeItsAverageRate()
        {
            var product = new Product("produto", "path", 8.5m, "test", Guid.NewGuid());
            
            int rateNumber = 5;
            int rateNumber2 = 8;

            var rate = new ProductReview(Guid.NewGuid(), "muito top", rateNumber);
            var rate2 = new ProductReview(Guid.NewGuid(), "muito top", rateNumber2);

            product.AddReview(rate);
            product.AddReview(rate2);

            product.RemoveReview(rate2);

            Assert.AreEqual(rateNumber, product.AverageRate);
        }        
    }
}
