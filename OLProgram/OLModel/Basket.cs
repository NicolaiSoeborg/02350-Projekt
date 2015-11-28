using System.Collections.ObjectModel;

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
