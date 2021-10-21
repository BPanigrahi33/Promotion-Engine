using NUnit.Framework;
using Promotion_Engine.manager;
using Promotion_Engine.Model;
using Promotion_Engine.Model.Carts;
using Promotion_Engine.Model.Promotions;
using System.Collections.Generic;
namespace PromotionTestProject.Promotions
{
    [TestFixture]
    public class AmountPromotionTest
    {
        private ICart testCart;
        private Product pA;
        private Product pB;
        private AmountPromotion amountPromotion1;
        private AmountPromotion amountPromotion2;

        [SetUp]
        public void Setup()
        {
            pA = new Product("A", 50);
            pB = new Product("B", 30);
            testCart = new BasicCart();
            amountPromotion1 = new AmountPromotion(pA, 3, 130);
            amountPromotion2 = new AmountPromotion(pB, 2, 45);
        }

        [Test]
        public void Test1()
        {
            testCart.Add(pA, 3);
            var discountedPrice = amountPromotion1.CalculateDiscount(testCart);
            Assert.AreEqual(20, discountedPrice);
        }

        [Test]
        public void Test2()
        {
            testCart.Add(pB, 2);
            var discountedPrice = amountPromotion2.CalculateDiscount(testCart);
            Assert.AreEqual(15, discountedPrice);
        }

        [Test]
        public void Test3()
        {
            testCart.Add(pB, 5);
            var discountedPrice = amountPromotion2.CalculateDiscount(testCart);
            Assert.AreEqual(30, discountedPrice);
        }
    }
}
