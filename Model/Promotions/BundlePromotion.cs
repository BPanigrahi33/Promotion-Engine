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

            foreach (KeyValuePair<Product, int> element in this.requiredItems)
            {
                var reqAmount = this.getRequiredProductAmount(element.Key);
                if (!cart.HasProduct(element.Key) || reqAmount > cart.GetProductAmount(element.Key))
                {
                    allProducts = false;
                }
            }

            return allProducts;
        }
        public bool IsApplicable(ICart cart)
        {
            return this.DoesCartHaveAllProducts(cart);
        }

        public Dictionary<ICart, int> CalculateDiscount(ICart cart)
        {
            Dictionary<ICart, int> dicData = new Dictionary<ICart, int>();
            if (!this.DoesCartHaveAllProducts(cart))
            {
                dicData.Add(cart, 0);
                return dicData;
            }

            int oldPrice = 0;
            int counter = 0;

            foreach (KeyValuePair<Product, int> requiredProduct in this.requiredItems)
            {
                counter++;
                var requiredAmount = this.requiredItems.GetValueOrDefault(requiredProduct.Key)!;
                var totalPrice = requiredProduct.Value * requiredAmount;
                oldPrice = oldPrice + totalPrice;
                cart.UpdateProductAmount(requiredProduct.Key, requiredAmount);
                if (counter < this.requiredItems.Count())
                {
                    cart.UpdateProductPrice(requiredProduct.Key, 0);
                }
                else
                {
                    cart.UpdateProductPrice(requiredProduct.Key, this.bundlePrice);
                }
            }

            var discount = oldPrice - this.bundlePrice;
            dicData.Add(cart, discount);
            return dicData;
        }

        public string GetOverview()
        {
            string overview = "Bundle promotion( + " + this.bundlePrice + ")";
            foreach (KeyValuePair<Product, int> requiredProduct in this.requiredItems)
            {
                overview += requiredProduct.Key.Sku;
            }

            return overview;
        }
    }
}
