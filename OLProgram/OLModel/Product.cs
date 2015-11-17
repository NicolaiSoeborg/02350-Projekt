using System;
using System.Windows;
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
        public string ProductName { get; } // TODO: Skal der være en setter? Nej, vel? Eller kan man så ikke ændre navn fra admin panel?
        public string ImageFileName { get; }
        public int Stock { get; set; }
        public int Bought { get; set; }

        public Product(string ProductName) : this(ProductName, "default.png") { }
        
        public Product(string ProductName, string ImageFileName)
        {
            ProductIdCounter++;
            this.ProductId = ProductIdCounter++;
            this.ProductName = ProductName;
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

        public void Increase(Product product) { Increase(product, 1); }

        public void Increase(Product product, int count)
        {
            foreach (BasketItem item in BasketItems)
            {
                if (item.Product.ProductId == product.ProductId)
                {
                    item.SetCount(item.Count + count);
                    //item.Count += count;
                    return;
                }
            }
            BasketItems.Add(new BasketItem(product, count));
        }

        public void Decrease(Product product, int Count)
        {
            foreach (BasketItem item in BasketItems)
            {
                if (item.Product == product)
                {
                    item.Count -= Count;
                    if (item.Count < 1)
                    {
                        BasketItems.Remove(item);
                    }
                    return;
                }
            }
        }
    }

    public class BasketItem : NotifyBase
    {
        public Product Product { get; }
        public int Count { get; set; }
        //private int _Count;
        //public int Count { get { return _Count; } set { _Count = value; NotifyPropertyChanged();} }

        private String _CountAndName;
        public String CountAndName { get { return _CountAndName; } set { _CountAndName = value; NotifyPropertyChanged(); } }

        public BasketItem(Product Product) : this(Product, 1) { }

        public BasketItem(Product Product, int Count)
        {
            this.Product = Product;
            this.Count = Math.Max(Count, 1);
            this.CountAndName = "" + this.Count + " x " + Product.ProductName;
        }

        public void SetCount(int NewCount)
        {
            Count = NewCount;
            CountAndName = "" + this.Count + " x " + Product.ProductName;
        }


    }
}