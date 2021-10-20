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
            cart.GetCartItems().forEach((product, amount) =>
            {
                totalPrice = totalPrice + product.price * amount;
            });
            return totalPrice;
        }
    }
}
}
