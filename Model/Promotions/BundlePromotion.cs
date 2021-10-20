using Promotion_Engine.Model.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Model.Promotions
{
    public class BundlePromotion : IPromotion
    {
        public int bundlePrice { get; set; }
        public Dictionary<Product, int> requiredItems { get; set; }
        public BundlePromotion(Dictionary<Product, int> requiredItems, int price)
        {
            this.requiredItems = requiredItems;
            this.bundlePrice = price;
        }

        public void addSingleRequiredItem(Product product)
        {
            this.requiredItems.Add(product, 1);
        }
        public void addRequiredItem(Product product, int amount)
        {
            this.requiredItems.Add(product, amount);
        }

        public int getRequiredProductAmount(Product product)
        {
            if (this.requiredItems.ContainsKey(product))
            {
                return this.requiredItems.GetValueOrDefault(product);
            }
            else
            {
                return 0;
            }
        }
        public bool DoesCartHaveAllProducts(ICart cart)
        {
            bool allProducts = true;
            this.requiredItems.Keys(element =>
            {
                const reqAmount = this.getRequiredProductAmount(element);
                if (
                  !cart.hasProduct(element) ||
                  reqAmount > cart.getProductAmount(element)
                )
                {
                    allProducts = false;
                }
            });
            return allProducts;
        }
        public bool IsApplicable(ICart cart)
        {
            return this.DoesCartHaveAllProducts(cart);
        }

        public Dictionary<ICart, int> CalculateDiscount(ICart cart)
        {
            if (!this.DoesCartHaveAllProducts(cart))
            {
                return [cart, 0];
            }

            int oldPrice = 0;
            int counter = 0;
            this.requiredItems.forEach(requiredProduct =>
            {
                counter++;
                var requiredAmount = this.requiredItems.getValue(requiredProduct)!;
                var totalPrice = requiredProduct.Price * requiredAmount;
                oldPrice = oldPrice + totalPrice;
                cart.updateProductAmount(requiredProduct, requiredAmount);
                if (counter < this.requiredItems.size())
                {
                    cart.updateProductPrice(requiredProduct, 0);
                }
                else
                {
                    cart.updateProductPrice(requiredProduct, this.bundlePrice);
                }
            });

            var discount = oldPrice - this.bundlePrice;
            return [cart, discount];
        }

        public string GetOverview()
        {
            string overview = "Bundle promotion( + " + this.bundlePrice + ")";
            this.requiredItems.forEach(requiredProduct => {
                overview += requiredProduct.Sku;
            });
            return overview;
        }
    }
}
