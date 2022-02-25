using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class OrdersController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;

        public OrdersController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public IActionResult Listing(string CategoriesId = "")
        {
            if (HttpContext.Session.GetString("username") == null) return RedirectToAction("Index", "Login");
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                conn.Open();  // Open the connection  
                SqlCommand cmd = new SqlCommand("select Orders.InvoiceNo,Orders.OrderPlaced,Products.ProductTitle,Orders.Quantity,Orders.UnitTitle,Orders.TotalPrice,Orders.Discount, Orders.Taxes, Orders.NetTotal, Orders.OrderStatus from Orders", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewBag.categories = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                ViewBag.errmsg = "Login Failed" + ex.Message;

            }
            return View("~/Views/Admin/Cutomers/CustomerList.cshtml");

        }
    }
}
