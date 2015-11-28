using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
    public class BasketItem : Product
    {
        public int Count { get; set; }

        private String _countAndName;
        public String CountAndName { get { return _countAndName; } set { _countAndName = value; NotifyPropertyChanged(); } }

        public BasketItem(Product Product) : this(Product, 1) { }

        public BasketItem(Product Product, int Amount) : base(Product)
        {
            Count = Math.Max(Amount, 1);
            CountAndName = String.Format("{0} x {1}", Count, Product.ProductName);
        }

        public void SetCount(int NewCount)
        {
            Count = NewCount;
            CountAndName = String.Format("{0} x {1}", Count, ProductName);
        }
    }
}
