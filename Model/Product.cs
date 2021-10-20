using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Model
{
    public class Product
    {
        public int Price { get; set; }
        public string Sku { get; set; }
        public Product(string Sku, int Price)
        {
            this.Sku = Sku;
            this.Price = Price;
        }

        public override string ToString()
        {
            return this.Sku;
        }
    }
}
