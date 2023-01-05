using Microsoft.EntityFrameworkCore.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public  class Purchase
    {

        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }
        public string IdType { get; set; }
        public string Id { get; set; }
        public string ClientName { get; set; }
       
        public  ICollection<ProductPurchase> ProductPurchases { get; set; }
    }
}
