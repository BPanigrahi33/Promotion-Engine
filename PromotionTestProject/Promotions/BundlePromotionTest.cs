using NUnit.Framework;
using Promotion_Engine.manager;
using Promotion_Engine.Model;
using Promotion_Engine.Model.Carts;
using Promotion_Engine.Model.Promotions;
using System.Collections.Generic;
namespace PromotionTestProject.Promotions
{
    [TestFixture]
    public class BundlePromotionTest
    {
        private ICart testCart;
        private Product pC;
        private Product pD;
        private BundlePromotion bundlePromotion1;
        private Dictionary<Product, int> discountedItems;

        [SetUp]
        public void Setup()
        {
            pC = new Product("C", 20);
            pD = new Product("D", 15);
            testCart = new BasicCart();

            discountedItems = new Dictionary<Product, int>();
            discountedItems.Add(pC, 1);
            discountedItems.Add(pD, 1);

            bundlePromotion1 = new BundlePromotion(discountedItems, 30);
        }

        [Test]
        public void Test1()
        {
            testCart.Add(pC, 1);
            testCart.Add(pD, 1);
            var discountedPrice = bundlePromotion1.CalculateDiscount(testCart);
            Assert.AreEqual(5, discountedPrice);
        }
    }
}
