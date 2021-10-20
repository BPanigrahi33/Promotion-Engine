using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Model.Carts
{
    public interface ICart
    {
        public void Add(Product product, int amount);
        public void Remove(Product product, int amount);
        public int GetUniqueCount();
        public int GetTotalCount();
        public Dictionary<Product, int> GetCartItems();
        public int GetProductAmount(Product product);
        public Boolean HasProduct(Product product);
        public void UpdateProductAmount(Product product, int newAmount);
        public void UpdateProductPrice(Product product, int price);
        public Carts.ICart Clone();
    }
}
