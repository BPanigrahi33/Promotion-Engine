using Promotion_Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion_Engine.Model.Carts
{
    public class BasicCart : ICart, IHasOverview
    {
        private Dictionary<Product, int> cartItems;
        public BasicCart()
        {
            this.cartItems = new Dictionary<Product, int>();
        }
        public void Add(Product product, int amount)
        {
            int newAmount = amount;
            if (this.cartItems.ContainsKey(product))
            {
                var oldAmount = this.cartItems.GetValueOrDefault(product);
                newAmount = oldAmount + newAmount;
            }
            this.cartItems.Add(product, newAmount);
        }

        public ICart Clone()
        {
            var newCart = new BasicCart();
            foreach (KeyValuePair<Product, int> element in this.cartItems)
            {
                newCart.Add(element.Key, element.Value);
            }

            return newCart;
        }

        public Dictionary<Product, int> GetCartItems()
        {
            return this.cartItems;
        }

        public string GetOverview()
        {
            string overview = null;
            foreach (KeyValuePair<Product, int> element in this.cartItems)
            {
                overview = element.Key.Sku + "*" + element.Value + "(total " + element.Key.Price + "*" + element.Value + ")";
            }

            return overview;
        }

        public int GetProductAmount(Product product)
        {
            if (this.cartItems.ContainsKey(product))
            {
                return this.cartItems.GetValueOrDefault(product);
            }
            else
            {
                return 0;
            }
        }

        public int GetTotalCount()
        {
            int totalCount = 0;
            foreach (KeyValuePair<Product, int> element in this.cartItems)
            {
                totalCount = totalCount + element.Value;
            }

            return totalCount;
        }

        public int GetUniqueCount()
        {
            return this.cartItems.Count();
        }

        public bool HasProduct(Product product)
        {
            return this.GetCartItems().ContainsKey(product);
        }

        public void Remove(Product product, int amount)
        {
            if (this.cartItems.ContainsKey(product))
            {
                var oldAmount = this.cartItems.GetValueOrDefault(product);
                var newAmount = oldAmount - amount >= 0 ? oldAmount - amount : 0;

                this.UpdateProductAmount(product, newAmount);
            }
        }

        public void UpdateProductAmount(Product product, int newAmount)
        {
            if (newAmount > 0)
            {
                this.cartItems.Add(product, newAmount);
            }
            else
            {
                this.cartItems.Remove(product);
            }
        }

        public void UpdateProductPrice(Product product, int price)
        {
            var amount = this.cartItems.GetValueOrDefault(product)!;
            this.cartItems.Remove(product);
            product.Price = price;
            this.cartItems.Add(product, amount);
        }
    }
}
