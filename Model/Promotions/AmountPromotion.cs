using Promotion_Engine.Model.Carts;
using Promotion_Engine.Model.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Model.Promotions
{
    public class AmountPromotion : IPromotion
    {
        public Product Products { get; set; }
        public int Amount { get; set; }
        public int DiscountedPrice { get; set; }

        public AmountPromotion(Product Products, int Amount, int DiscountedPrice)
        {
            this.Products = Products;
            this.Amount = Amount;
            this.DiscountedPrice = DiscountedPrice;
        }
        public Dictionary<ICart, int> CalculateDiscount(ICart cart)
        {
            int discount = 0;
            Dictionary<ICart, int> dicData = new Dictionary<ICart, int>();

            if (cart.GetCartItems().ContainsKey(this.Products))
            {
                int cartAmount = cart.GetCartItems().GetValueOrDefault(this.Products);

                if (cartAmount >= this.Amount)
                {
                    var amountOfDiscountBundles = Math.Abs(cartAmount / this.Amount);
                    var newPrice = amountOfDiscountBundles * this.DiscountedPrice + this.Products.Price * (cartAmount - amountOfDiscountBundles * this.Amount);
                    var oldPrice = cart.GetCartItems().GetValueOrDefault(this.Products) * this.Products.Price;
                    discount = oldPrice - newPrice;
                    cart.UpdateProductAmount(this.Products, (cartAmount - amountOfDiscountBundles * this.Amount));
                }

                dicData.Add(cart, discount);
            }
            return dicData;
        }

        public string GetOverview()
        {
            return "Amount promotion:" + this.Amount + "*" + this.Products.Sku + "=" + this.DiscountedPrice;
        }

        public bool IsApplicable(ICart cart)
        {
            if (cart.GetCartItems().ContainsKey(this.Products))
            {
                var cartAmount = cart.GetCartItems().GetValueOrDefault(this.Products);
                return cartAmount >= this.Amount;
            }

            return false;
        }
    }
}
