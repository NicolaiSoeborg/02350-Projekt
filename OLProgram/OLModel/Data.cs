using System;
using System.Collections.Generic;

namespace OLModel
{
    [Serializable]
    public class Data
    {
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
    }
}
