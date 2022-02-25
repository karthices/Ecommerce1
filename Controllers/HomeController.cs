using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                SqlCommand cmd = new SqlCommand("select * from products", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                ViewBag.BestSellers = dt;

            }
            catch (Exception ex)
            {
                conn.Close();
                ViewBag.errmsg = "Login Failed" + ex.Message;

            }

            return View();
        }

        public IActionResult details(ProductModel productModel)
        {
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                SqlCommand cmd = new SqlCommand("select * from products where productid="+productModel.ProductId, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                ViewBag.Product = dt;

            }
            catch (Exception ex)
            {
                conn.Close();
                ViewBag.errmsg = "Login Failed" + ex.Message;

            }
            return View("~/Views/Home/Detail.cshtml");
        }


    }
}
