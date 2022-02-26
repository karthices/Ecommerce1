using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
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
                dt = new DataTable();
                cmd = new SqlCommand("select *, (select count(*) from products where ProductCategory = CategoriesId) as productcount  from categories where ispopular = 1", conn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                ViewBag.Popular = dt;

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

                SqlCommand cmd = new SqlCommand("select * from products where productid=" + productModel.ProductId, conn);
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

        public IActionResult Listing(string ListingType = "", int listingid = 0)
        {
            if (ListingType == "Categories")
            {
                SqlConnection conn = new SqlConnection(connetionString);
                try
                {

                    SqlCommand cmd = new SqlCommand("select * from products where productcategory = " + listingid, conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    ViewBag.SearchResult = dt;

                }
                catch (Exception ex)
                {
                    conn.Close();
                    ViewBag.errmsg = "Login Failed" + ex.Message;

                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View("~/Views/Home/listing.cshtml");
        }

        public IActionResult addtocart(int productid = 0, int quantity = 0)
        {
            if (productid != 0 && quantity != 0)
            {
                SqlConnection conn = new SqlConnection(connetionString);
                try
                {
                    bool isLoggedIn = false;
                    bool IsProcessed = false;
                    Random rnd = new Random();
                    string RandomCustomer = rnd.Next().ToString().Replace("-", "");
                    if (HttpContext.Session.GetString("guestuser") != null)
                    {
                        isLoggedIn = true;
                        if (HttpContext.Session.GetString("RandomCustomer") != null)
                        {
                            RandomCustomer = HttpContext.Session.GetString("RandomCustomer");
                        }
                    }
                    else
                    {
                        if (HttpContext.Session.GetString("RandomCustomer") != null)
                        {
                            RandomCustomer = HttpContext.Session.GetString("RandomCustomer");
                        }
                        else
                        {
                            HttpContext.Session.SetString("RandomCustomer", RandomCustomer);
                        }

                    }
                    SqlCommand cmd;

                    cmd = new SqlCommand("select * from cart where productid = '" + productid+"' and randomcustomer='"+RandomCustomer+"' and isprocessed=0", conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    conn.Open();  // Open the connection  
                    if(dt.Rows.Count == 0)
                    {

                    cmd = new SqlCommand("insert into Cart(RandomCustomer,ProductId, IsLoggedIn, Quantity, IsProcessed)values('" + RandomCustomer + "','" + productid + "','" + isLoggedIn + "','" + quantity + "','" + IsProcessed + "');", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Update Cart set Quantity='" + (quantity+Convert.ToInt32(dt.Rows[0]["quantity"])) + "' where cartid = '" + dt.Rows[0]["cartid"].ToString()  + "';", conn);
                    }


                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
                catch (Exception ex)
                {
                    conn.Close();

                }
            }

            return Json(new { status = "true" });
        }

        public IActionResult cart()
        {
            return View("~/Views/Home/cart.cshtml");
        }


    }
}
