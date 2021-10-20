using Promotion_Engine.Model.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Util
{
    public interface ICartPriceCalculator
    {
        public int getTotalPrice(ICart cart);
    }
}
