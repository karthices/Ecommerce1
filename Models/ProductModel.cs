using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class ProductModel
    {
        public IFormFile ProductImage1 { get; set; }
        public string ProductImagePath1 { get; set; }
        public IFormFile ProductImage2 { get; set; }
        public string ProductImagePath2 { get; set; }
        public IFormFile ProductImage3 { get; set; }
        public string ProductImagePath3 { get; set; }
        public IFormFile ProductImage4 { get; set; }
        public string ProductImagePath4 { get; set; }
        public IFormFile ProductImage5 { get; set; }
        public string ProductImagePath5 { get; set; }


        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductCategory { get; set; }
        public string Quantity { get; set; }
        public string Units { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string Tax { get; set; }
        public string IsActive { get; set; }
    }
}
