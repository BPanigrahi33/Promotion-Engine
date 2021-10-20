using Promotion_Engine.Model;
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

            promotions.ToList().ForEach(promotion =>
            {
                if (promotion.IsApplicable(discountedCart))
                {
                    Dictionary<ICart, int> promotionResult = promotion.CalculateDiscount(discountedCart);
                    foreach (KeyValuePair<ICart, int> element in promotionResult)
                    {
                        discount = discount + element.Value;
                        discountedCart = element.Key;
                    }
                }
            });

            return discount;
        }
    }
}
