using Promotion_Engine.Model.Carts;
using Promotion_Engine.Model.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Util
{
    public interface IPromotionEngine
    {
        public int getDiscountedPrice(ICart cart, IPromotion[] promotions);
    }
}
