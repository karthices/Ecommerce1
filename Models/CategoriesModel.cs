using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class CategoriesModel
    {
        public IFormFile CategoriesImage { get; set; }
        public string CategoriesImagePath { get; set; }
     
        public string CategoriesId { get; set; }
        public string CategoriesTitle { get; set; }
        public string ParentCategory { get; set; }
        public string IsActive { get; set; }
    }
}
