using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
        public class Basket
        {
            public ObservableCollection<BasketItem> BasketItems { get; set; }

            public Basket()
            {
                BasketItems = new ObservableCollection<BasketItem>();
            }

            public void Increase(Product product, int count)
            {
                foreach (BasketItem item in BasketItems)
                {
                    if (item.ProductId == product.ProductId)
                    {
                        item.SetCount(item.Count + count);
                        return;
                    }
                }
                BasketItems.Add(new BasketItem(product, count));
            }

            public int getCount(Product product)
            {
                foreach (BasketItem bItem in BasketItems)
                {
                    if (bItem.ProductId == product.ProductId)
                        return bItem.Count;
                }
                return 0;
            }

        }
    
}
