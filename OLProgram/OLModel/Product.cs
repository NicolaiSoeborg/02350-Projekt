using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
    public class Product : NotifyBase
    {
        // Overvej at slette, da den ikke bliver genmt ned i XML og derved potentielt kan fucke det op
        private static int ProductIdCounter = 0;

        public int ProductId { get; set; }
        public string ProductName { get; set; } // TODO: Skal der være en setter? Nej, vel? Eller kan man så ikke ændre navn fra admin panel?
        public string ImageFileName { get; }
        public int Stock { get; set; }
        public int Bought { get; set; }

        public Product(string ProductName) : this(ProductName, "default.png") { }

        // This Constructor is used for making basketitems, making clone of the parameter Product
        public Product(Product Product)
        {
            ProductId = Product.ProductId;
            ProductName = Product.ProductName;
            ImageFileName = Product.ImageFileName;
        }

        public Product(string ProductName, string ImageFileName)
        {
            ProductIdCounter++;
            ProductId = ProductIdCounter++;
            this.ProductName = ProductName;
            this.ImageFileName = ImageFileName;
        }

        public override string ToString()
        {
            return ("" + this.ProductId + " " + this.ProductName);
        }
    }

    public class BasketItem : Product
    {
        public int Count { get; set; }

        private String _CountAndName;
        public String CountAndName { get { return _CountAndName; } set { _CountAndName = value; NotifyPropertyChanged(); } }

        public BasketItem(Product Product) : this(Product, 1) { }

        public BasketItem(Product Product, int Amount) : base(Product)
        {
            Count = Math.Max(Amount, 1);
            CountAndName = "" + Count + " x " + Product.ProductName;
        }

        public void SetCount(int NewCount)
        {
            Count = NewCount;
            CountAndName = "" + Count + " x " + ProductName;
        }
    }

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
            foreach(BasketItem bItem in BasketItems)
            {
                if (bItem.ProductId == product.ProductId)
                    return bItem.Count;
            }
            return 0;
        }

       
    }


}