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
    public class ProductController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ProductController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public IActionResult Listing(string ProductId = "")
        {
            if (HttpContext.Session.GetString("username") == null) return RedirectToAction("Index", "Login");
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                conn.Open();  // Open the connection  
                SqlCommand cmd = new SqlCommand("select productId,ProductTitle,ProductImage1,ProductImage2,ProductImage3,ProductImage4,ProductImage5,ProductCategory,isactive from Products order by ProductId desc;", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewBag.categories = dt;

                cmd = new SqlCommand("select * from Products where ProductCategory=0 order by ProductTitle asc ;", conn);
                dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewBag.parentcategory = dt;

                if (!string.IsNullOrEmpty(ProductId))
                { 
                    cmd = new SqlCommand("select * from Products where productId='" + ProductId + "';", conn);
                    dt = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    ViewBag.editcategory = dt;
                }


                conn.Close();


            }
            catch (Exception ex)
            {
                conn.Close();
                ViewBag.errmsg = "Login Failed" + ex.Message;

            }
            return View("~/Views/Admin/Products/ProductList.cshtml");

        }

        public async Task<IActionResult> SaveProduct(ProductModel prodObj)
        {
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {
                string newfilename1 = "",newfilename2= "",newfilename3 = "",newfilename4 = "",newfilename5 = "";
                string savedpath1 = "",savedpath2 = "",savedpath3 = "",savedpath4 = "",savedpath5 = "";
                if (prodObj.ProductImage1 != null)
                {
                    newfilename1 = prodObj.ProductTitle.Replace(" ", "_")+"-1."+ prodObj.ProductImage1.FileName.Split(".")[prodObj.ProductImage1.FileName.Split(".").Length-1];
                    savedpath1 = Path.Combine("uploads\\product\\", newfilename1);
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "product");

                    string filePath = Path.Combine(uploads, newfilename1);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await prodObj.ProductImage1.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    savedpath1 = prodObj.ProductImagePath1;
                }

                if (prodObj.ProductImage2 != null)
                {
                    newfilename2 = prodObj.ProductTitle.Replace(" ", "_")+"-2." + prodObj.ProductImage2.FileName.Split(".")[prodObj.ProductImage2.FileName.Split(".").Length - 1];
                    savedpath2 = Path.Combine("uploads\\product\\", newfilename2);
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "product");

                    string filePath = Path.Combine(uploads, newfilename2);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await prodObj.ProductImage2.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    savedpath2 = prodObj.ProductImagePath2;
                }
                
                if (prodObj.ProductImage3 != null)
                {
                    newfilename3 = prodObj.ProductTitle.Replace(" ", "_") + "-3." + prodObj.ProductImage3.FileName.Split(".")[prodObj.ProductImage3.FileName.Split(".").Length - 1];
                    savedpath3 = Path.Combine("uploads\\product\\", newfilename3);
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "product");

                    string filePath = Path.Combine(uploads, newfilename3);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await prodObj.ProductImage3.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    savedpath3 = prodObj.ProductImagePath3;
                }
                
                if (prodObj.ProductImage4 != null)
                {
                    newfilename4 = prodObj.ProductTitle.Replace(" ", "_") + "-4." + prodObj.ProductImage4.FileName.Split(".")[prodObj.ProductImage4.FileName.Split(".").Length - 1];
                    savedpath4 = Path.Combine("uploads\\product\\", newfilename4);
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "product");

                    string filePath = Path.Combine(uploads, newfilename4);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await prodObj.ProductImage4.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    savedpath4 = prodObj.ProductImagePath4;
                }
                
                if (prodObj.ProductImage5 != null)
                {
                    newfilename5 = prodObj.ProductTitle.Replace(" ", "_") + "-5." + prodObj.ProductImage5.FileName.Split(".")[prodObj.ProductImage5.FileName.Split(".").Length - 1];
                    savedpath5 = Path.Combine("uploads\\product\\", newfilename5);
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "product");

                    string filePath = Path.Combine(uploads, newfilename5);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await prodObj.ProductImage5.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    savedpath5 = prodObj.ProductImagePath5;
                }

                SqlCommand cmd;

                conn.Open();  // Open the connection  
                if(string.IsNullOrEmpty(prodObj.ProductId))
                {

                cmd = new SqlCommand("insert into Products(ProductTitle,ProductImage1,ProductImage2,ProductImage3,ProductImage4,ProductImage5,ProductSlug,ProductCategory,Quantity,Units,Price ,Discount,Tax,IsActive)values('" + prodObj.ProductTitle + "','" + savedpath1 + "','" + savedpath2 + "','" + savedpath3 + "','" + savedpath4 + "','" + savedpath5 + "','ss','"+ prodObj.ProductCategory + "','" + prodObj.Quantity + "','" + prodObj.Units + "','" + prodObj.Price + "','" + prodObj.Discount + "','" + prodObj.Tax + "','" + prodObj.IsActive + "');", conn);
                }
                else
                {
                    cmd = new SqlCommand("update Products set ProductTitle='" + prodObj.ProductTitle + "',ProductImage1='" + savedpath1 + "',ProductImage2='" + savedpath2 + "',ProductImage3='" + savedpath3 + "',ProductImage4='" + savedpath4 + "',ProductImage5='" + savedpath5 + "',IsActive='" + prodObj.IsActive + "' where ProductId='" + prodObj.ProductId + "';", conn);
                }

                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                conn.Close();

            }
            return RedirectToAction("Listing");
        }

        public IActionResult Delete(String ProductId)
        {
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                conn.Open();  // Open the connection  
                SqlCommand cmd = new SqlCommand("select * from Products where ProductId='" + ProductId + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                 cmd = new SqlCommand("delete from Products where productId='" + ProductId + "'", conn);
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(dt.Rows[0]["ProductImage1"].ToString()) && System.IO.File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, dt.Rows[0]["ProductImage1"].ToString())))
                {
                    System.IO.File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, dt.Rows[0]["ProductImage1"].ToString()));
                }
                conn.Close();
                HttpContext.Session.SetString("succssmsg", "Deleted Successfully");


            }
            catch (Exception ex)
            {
                conn.Close();
                HttpContext.Session.SetString("errmsg", "Login Failed" + ex.Message);
            }
            return RedirectToAction("Listing", "Products");
        }
    }
}
