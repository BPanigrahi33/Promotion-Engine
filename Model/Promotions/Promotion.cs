using Promotion_Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Promotion_Engine.Model.Carts;

namespace Promotion_Engine.Model.Promotions
{
    public interface IPromotion : IHasOverview
    {
        public Boolean IsApplicable(ICart cart);
        public Dictionary<ICart, int> CalculateDiscount(ICart cart);
    }

    public abstract class PromotionBase
    {
        public override string ToString()
        {
            return this.ToString();
        }
    }
}
