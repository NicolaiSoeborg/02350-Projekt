using System;

namespace OLModel
{
    public class BasketItem : Product
    {
        public int Count { get; set; }

        private String _countAndName;
        public String CountAndName { get { return _countAndName; } set { _countAndName = value; NotifyPropertyChanged(); } }

        public BasketItem(Product product) : this(product, 1) { }

        public BasketItem(Product product, int amount) : base(product)
        {
            Count = Math.Max(amount, 1);
            CountAndName = String.Format("{0} x {1}", Count, product.ProductName);
        }

        public void SetCount(int newCount)
        {
            Count = newCount;
            CountAndName = String.Format("{0} x {1}", Count, ProductName);
        }
    }
}
