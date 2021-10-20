using Promotion_Engine.Model;
using Promotion_Engine.Model.Carts;
using Promotion_Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.manager
{
    public class CartPriceCalculator : ICartPriceCalculator
    {
        public int getTotalPrice(ICart cart)
        {
            int totalPrice = 0;
            foreach (KeyValuePair<Product, int> element in cart.GetCartItems())
            {
                totalPrice = totalPrice + element.Key.Price * element.Value;
            }

            return totalPrice;
        }
    }
}

