using NUnit.Framework;
using Promotion_Engine.Model;
using Promotion_Engine.Model.Carts;

namespace PromotionTestProject
{
    [TestFixture]
    public class ProductTest
    {
        private ICart testCart;
        private Product result1;
        private Product result2;

        [SetUp]
        public void Setup()
        {
            result1 = new Product("A", 50);
            result2 = new Product("B", 50);
            testCart = new BasicCart();
        }

        [Test]
        public void Test1()
        {
            // should create a Product A with price 50.
            Assert.AreEqual("A", result1.Sku);
            Assert.AreEqual(50, result1.Price);
        }

        [Test]
        public void Test2()
        {
            // should create a Product B with price 50 and change to 30.
            Assert.AreEqual("B", result2.Sku);
            Assert.AreEqual(50, result2.Price);

            result2.Price = 30;
            Assert.AreEqual(30, result2.Price);
        }
    }
}
