using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class OrdersModel
    {
        public string InvoiceNo { get; set; }
        public string OrderPlaced { get; set; }
        public string ProductTitle { get; set; }
        public string Quantity { get; set; }
        public string UnitTitle { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string Taxes { get; set; }
        public string NetTotal { get; set; }
        public string OrderStatus { get; set; }
        public string IsActive { get; set; }
    }
}
