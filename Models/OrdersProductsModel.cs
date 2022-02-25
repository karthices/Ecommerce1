using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class OrdersProductsModel
    {
        public string OrderedProductId { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PriceId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string SellingPrice { get; set; }
        public string OfferPrice { get; set; }
        public string Quantity { get; set; }
        public string TotalAmount { get; set; }
        public string Tax { get; set; }
        public string NetTotal { get; set; }
        public string IsActive { get; set; }
    }
}
