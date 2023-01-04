using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Product
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int InInventory { get; set; }
        public bool isEnabled { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set;}


    }
}
