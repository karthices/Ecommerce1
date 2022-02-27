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

                    cmd = new SqlCommand("select * from cart where productid = '" + productid + "' and randomcustomer='" + RandomCustomer + "' and isprocessed=0", conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    conn.Open();  // Open the connection  
                    if (dt.Rows.Count == 0)
                    {

                        cmd = new SqlCommand("insert into Cart(RandomCustomer,ProductId, IsLoggedIn, Quantity, IsProcessed)values('" + RandomCustomer + "','" + productid + "','" + isLoggedIn + "','" + quantity + "','" + IsProcessed + "');", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Update Cart set Quantity='" + (quantity + Convert.ToInt32(dt.Rows[0]["quantity"])) + "' where cartid = '" + dt.Rows[0]["cartid"].ToString() + "';", conn);
                    }

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cmd = new SqlCommand("select sum(quantity) as count from cart where  randomcustomer='" + RandomCustomer + "' and isprocessed=0", conn);
                    dt = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    int qty = 0;
                    if (dt.Rows.Count > 0 && dt.Rows[0]["count"] != DBNull.Value)
                    {
                        qty = Convert.ToInt32(dt.Rows[0]["count"]);
                    }


                    return Json(new { status = "true", cartqty = qty });

                }
                catch (Exception ex)
                {
                    conn.Close();
                    return Json(new { status = "false", msg = "error :" + ex.Message });

                }
            }
            else
            {
                return Json(new { status = "false", msg = "error :invalid product and quantity" });

            }

        }

        public IActionResult updatecart(int productid = 0, int quantity = 0)
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

                    cmd = new SqlCommand("select * from cart where productid = '" + productid + "' and randomcustomer='" + RandomCustomer + "' and isprocessed=0", conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    conn.Open();  // Open the connection  
                    if (dt.Rows.Count > 0)
                    {

                        cmd = new SqlCommand("Update Cart set Quantity='" + quantity + "' where cartid = '" + dt.Rows[0]["cartid"].ToString() + "';", conn);

                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                    return this.getcart();

                }
                catch (Exception ex)
                {
                    conn.Close();
                    return Json(new { status = "false", msg = "error :" + ex.Message });

                }
            }
            else
            {
                return Json(new { status = "false", msg = "error :invalid product and quantity" });

            }
        }

        public IActionResult removecart(int productid = 0)
        {
            if (productid != 0)
            {
                SqlConnection conn = new SqlConnection(connetionString);
                try
                {

                    Random rnd = new Random();
                    string RandomCustomer = rnd.Next().ToString().Replace("-", "");
                    if (HttpContext.Session.GetString("guestuser") != null)
                    {
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
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from Cart where productid = '" + productid + "' and randomcustomer='" + RandomCustomer + "' and isprocessed=0;", conn);
                    cmd.ExecuteNonQuery();


                    conn.Close();

                    cmd = new SqlCommand("select sum(quantity) as count from cart where randomcustomer='" + RandomCustomer + "' and isprocessed=0", conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    int qty = 0;
                    if (dt.Rows.Count > 0 && dt.Rows[0]["count"] != DBNull.Value)
                    {
                        qty = Convert.ToInt32(dt.Rows[0]["count"]);
                    }


                    return Json(new { status = "true", cartqty = qty });

                }
                catch (Exception ex)
                {
                    conn.Close();
                    return Json(new { status = "false", msg = "error :" + ex.Message });

                }
            }
            else
            {
                return Json(new { status = "false", msg = "error :invalid product and quantity" });

            }

        }

        public IActionResult cart()
        {
            if (HttpContext.Session.GetString("RandomCustomer") == null)
            {

            }
            else
            {
                string RandomCustomer = HttpContext.Session.GetString("RandomCustomer");

                SqlConnection conn = new SqlConnection(connetionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("select cart.productid,cart.quantity, ProductTitle,ProductImage1,Price from cart left join products on products.productId=cart.productid where isprocessed=0 and randomcustomer='" + RandomCustomer + "'", conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    ViewBag.Cart = dt;
                }

                catch (Exception ex)
                {
                    ViewBag.errmsg = "Login Failed" + ex.Message;
                }
            }
            return View("~/Views/Home/cart.cshtml");
        }

        public JsonResult getcart()
        {
            Random rnd = new Random();
            string RandomCustomer = rnd.Next().ToString().Replace("-", "");
            if (HttpContext.Session.GetString("guestuser") != null)
            {
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

            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                SqlCommand cmd = new SqlCommand("select * from cart left join products on products.productid=cart.productid where randomcustomer='" + RandomCustomer + "' and isprocessed=0", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                List<object> obj = new List<object>();
                int qty = 0;
                foreach(DataRow dr in dt.Rows)
                {
                    string subtotal = String.Format("{0:#,0.000}", Convert.ToInt32(dr["quantity"]) * Convert.ToDecimal(dr["price"]));
                    qty += Convert.ToInt32(dr["quantity"]);
                    obj.Add(new { productid = dr["productid"].ToString(), qty = dr["quantity"].ToString(),  subtotal = subtotal.ToString() });
                }
                return Json(new { status = "true", cartqty = qty, items = obj });
            }

            catch (Exception ex)
            {
                ViewBag.errmsg = "Login Failed" + ex.Message;
                return Json(new { status = "false", msg = ex.Message });
            }
        }


    }
}
