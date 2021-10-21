using NUnit.Framework;
using Promotion_Engine.Model;
using Promotion_Engine.Model.Carts;

namespace PromotionTestProject
{
    [TestFixture]
    public class CartTest
    {
        private ICart testCart;
        private Product pB;

        [SetUp]
        public void Setup()
        {
            pB = new Product("B", 30);
            testCart = new BasicCart();
        }

        [Test]
        public void Test1()
        {
            // Creates an empty cart.
            testCart.Add(null, 0);
            Assert.AreEqual(0, testCart.GetTotalCount());
            Assert.AreEqual(0, testCart.GetUniqueCount());
        }

        [Test]
        public void Test2()
        {
            // Creates a cart and adds products.
            testCart.Add(pB, 1);
            Assert.AreEqual(1, testCart.GetTotalCount());
            Assert.AreEqual(1, testCart.GetUniqueCount());
        }

        [Test]
        public void Test3()
        {
            // Creates a cart and updates/removes products.
            testCart.Add(pB, 1);
            testCart.Add(pB, 5);
            Assert.AreEqual(6, testCart.GetTotalCount());

            testCart.Remove(pB, 2);
            Assert.AreEqual(4, testCart.GetTotalCount());

            testCart.Remove(pB, 5);
            Assert.AreEqual(0, testCart.GetTotalCount());
        }

        [Test]
        public void Test4()
        {
            // shouldn't allow negative ammounts.
            testCart.Add(pB, 30);
            testCart.Remove(pB, 31);
            Assert.AreEqual(0, testCart.GetTotalCount());
            
            testCart.Add(pB, 30);
            testCart.UpdateProductAmount(pB, -1);
            Assert.AreEqual(0, testCart.GetTotalCount());
        }
    }
}