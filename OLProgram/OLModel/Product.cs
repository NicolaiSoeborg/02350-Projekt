using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
    public class Product
    {
        // Overvej at slette, da den ikke bliver genmt ned i XML og derved potentielt kan fucke det op
        private static int ProductIdCounter = 0;

        public int ProductId { get; }
        public string Name { get; } // TODO: Skal der være en setter? Nej, vel? Eller kan man så ikke ændre navn fra admin panel?
        public string ImageFileName { get; }
        public int Stock { get; set; }
        public int Bought { get; set; }

        public Product() : this(ProductIdCounter, "<NAME>", "default.png")
        {
            ProductIdCounter++;
        }

        public Product(int ProductID, string Name, string ImageFileName)
        {
            this.ProductId = ProductID;
            this.Name = Name;
            this.ImageFileName = ImageFileName;
        }
    }

    public class Basket
    {
        public ObservableCollection<BasketItem> BasketItems { get; }

        public Basket ()
        {
            BasketItems = new ObservableCollection<BasketItem>();
        }

        public void Increase(Product product, int count)
        {
            foreach (BasketItem item in BasketItems)
            {
                if (item.Product == product)
                {
                    item.Count += count;
                    return;
                }
            }
            BasketItems.Add(new BasketItem(product, count));
        }

        public void Decrease(Product product, int count)
        {
            foreach (BasketItem item in BasketItems)
            {
                if (item.Product == product)
                {
                    item.Count -= count;
                    if (item.Count < 1)
                    {
                        BasketItems.Remove(item);
                    }
                    return;
                }
            }
        }
    }

    public class BasketItem
    {
        public Product Product { get; }
        public int Count { get; set; }

        public BasketItem()
        {
            Count++;
        }

        public BasketItem(Product Product) : this(Product, 1) { }

        public BasketItem(Product Product, int Count)
        {
            this.Product = Product;
            this.Count = Math.Max(Count, 1);
        }
    }
}