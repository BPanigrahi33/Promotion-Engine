using NUnit.Framework;
using Promotion_Engine.manager;
using Promotion_Engine.Model;
using Promotion_Engine.Model.Carts;
using Promotion_Engine.Model.Promotions;
using System.Collections.Generic;

namespace PromotionTestProject.Manager
{
    [TestFixture]
    public class PromotionEngineTest
    {
        private ICart testCart;
        private PromotionEngine promotionEngine;
        private IPromotion[] activePromotions;
        private Product pA;
        private Product pB;
        private Product pC;
        private Product pD;
        private CartPriceCalculator cartManager;

        [SetUp]
        public void Setup()
        {
            pA = new Product("A", 50);
            pB = new Product("B", 30);
            pC = new Product("C", 20);
            pD = new Product("D", 15);    
            testCart = new BasicCart();
            promotionEngine = new PromotionEngine();

            Dictionary<Product, int> discountedItems = new Dictionary<Product, int>();
            discountedItems.Add(pC, 1);
            discountedItems.Add(pD, 1);

            BundlePromotion bundlePromotion = new BundlePromotion(discountedItems, 30);

            AmountPromotion amountPromotion1 = new AmountPromotion(pA, 3, 130);
            AmountPromotion amountPromotion2 = new AmountPromotion(pB, 2, 45);
            cartManager = new CartPriceCalculator();

            activePromotions = new IPromotion[]
            {
              bundlePromotion,
              amountPromotion1,
              amountPromotion2
            };
        }

        [Test]
        public void Test1()
        {
            testCart.Add(pA, 1);
            testCart.Add(pB, 1);
            testCart.Add(pC, 1);

            var result = promotionEngine.getDiscountedPrice(
              testCart,
              activePromotions
            );

            
            var discountedPrice = cartManager.getTotalPrice(testCart) - result;
            Assert.AreEqual(100, discountedPrice);
        }

        [Test]
        public void Test2()
        {
            testCart.Add(pA, 1);
            testCart.Add(pB, 1);
            testCart.Add(pC, 1);

            var result = promotionEngine.getDiscountedPrice(
              testCart,
              activePromotions
            );

            CartPriceCalculator cartManager = new CartPriceCalculator();
            var discountedPrice = cartManager.getTotalPrice(testCart) - result;
            Assert.AreEqual(370, discountedPrice);
        }

        [Test]
        public void Test3()
        {
            testCart.Add(pA, 3);
            testCart.Add(pB, 5);
            testCart.Add(pC, 1);
            testCart.Add(pD, 1);

            var result = promotionEngine.getDiscountedPrice(
              testCart,
              activePromotions
            );

            CartPriceCalculator cartManager = new CartPriceCalculator();
            var discountedPrice = cartManager.getTotalPrice(testCart) - result;
            Assert.AreEqual(280, discountedPrice);
        }
    }
}
