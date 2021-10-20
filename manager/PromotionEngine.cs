using Promotion_Engine.Model.Carts;
using Promotion_Engine.Model.Promotions;
using Promotion_Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.manager
{
    public class PromotionEngine : IPromotionEngine
    {
        public int getDiscountedPrice(ICart cart, IPromotion[] promotions)
        {
            int discount = 0;
            var discountedCart = cart.Clone();

            promotions.forEach(promotion =>
            {
                if (promotion.IsApplicable(discountedCart))
                {
                    var promotionResult = promotion.calculateDiscount(discountedCart);
                    discount = discount + promotionResult[0];
                    discountedCart = promotionResult[1];
                }
            });

            return discount;
        }
    }
}
