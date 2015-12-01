using System;
using System.Collections.Generic;

namespace OLModel
{
    [Serializable]
    public class Data
    {
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<String> ProductKeys { get; set; }
        public List<int> AmountBought { get; set; }
        public List<int> ProductsForEachUser { get; set; }
        public List<String> AdminLog { get; set; }
        public List<String> UserLog { get; set; }
    }
}
