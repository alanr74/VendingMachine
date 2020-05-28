using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Models
{
    public class StockStack
    {
        private Dictionary<Product, int> stockItems = new Dictionary<Product, int>();

        public bool AddItemsToStack(Dictionary<Product, int> Products)
        {
            foreach (var products in Products)
            {
                AddItemsToStack(products.Key, products.Value);
            }
            return true;
        }

        public bool AddItemsToStack(Product product)
        {
            if (stockItems.TryGetValue(product, out int numberInStack))
            {
                numberInStack += 1;
                stockItems[product] = numberInStack;
                return true;
            }

            stockItems[product] = 1;
            return true;
        }

        public bool AddItemsToStack(Product product, int quatity)
        {
            if (stockItems.TryGetValue(product, out int numberInStack))
            {
                numberInStack += quatity;
                stockItems[product] = numberInStack;
                return true;
            }

            stockItems[product] = quatity;
            return true;
        }

        public bool RemoveItemsFromStack(Dictionary<Product, int> products)
        {
            return true;
        }

        public bool RemoveItemsFromStack(Product product)
        {
            if (stockItems.TryGetValue(product, out int numberInStack))
            {
                numberInStack -= 1;
                if (numberInStack >= 0)
                {
                    stockItems[product] = numberInStack;
                    return true;
                }
            }

            return false;
        }

        public Dictionary<Product, int> GetItemInformation(int position)
        {
            var stockItem = stockItems.FirstOrDefault(x => x.Key.Position == position);
            if(stockItem.Key == null)
            {
                return null;
            }
            return new Dictionary<Product, int>() { { stockItem.Key, stockItem.Value } };
        }
    }
}
