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
    public class CategoriesController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;

        public CategoriesController(IWebHostEnvironment environment)
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
                SqlCommand cmd = new SqlCommand("select c1.categoriesId,c1.categoriestitle,c2.categoriestitle as parentcategory,c1.categoriesimage,c1.isactive from Categories c1 left join Categories c2 on c2.categoriesId=c1.parentcategory order by c1.CategoriesId desc;", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewBag.categories = dt;

                string query = "select * from Categories where ParentCategory=0 order by CategoriesTitle asc ;";
                //if (employeePermission)
                //{
                //    query = "oen";
                //}
                
                cmd = new SqlCommand(query, conn);
                dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewBag.parentcategory = dt;

                if (!string.IsNullOrEmpty(CategoriesId))
                { 
                    cmd = new SqlCommand("select * from Categories where categoriesId='" + CategoriesId + "';", conn);
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
            return View("~/Views/Admin/Categories/CategoriesList.cshtml");

        }

        public async Task<IActionResult> SaveCategory(CategoriesModel categorObj)
        {
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {
                string newfilename = "";
                string savedpath = "";
                if (categorObj.CategoriesImage != null)
                {
                    newfilename = categorObj.CategoriesTitle.Replace(" ", "_")+"."+ categorObj.CategoriesImage.FileName.Split(".")[categorObj.CategoriesImage.FileName.Split(".").Length-1];
                    savedpath = Path.Combine("uploads\\category", newfilename);
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "category");

                    string filePath = Path.Combine(uploads, newfilename);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await categorObj.CategoriesImage.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    savedpath = categorObj.CategoriesImagePath;
                }

                SqlCommand cmd;

                conn.Open();  // Open the connection  
                if(string.IsNullOrEmpty(categorObj.CategoriesId))
                {

                cmd = new SqlCommand("insert into Categories(CategoriesTitle,ParentCategory,CategoriesImage,CategoriesSlug,IsActive)values('" + categorObj.CategoriesTitle + "','" + categorObj.ParentCategory + "','" + savedpath + "','','" + categorObj.IsActive + "');", conn);
                }
                else
                {
                    cmd = new SqlCommand("update Categories set CategoriesTitle='"+ categorObj.CategoriesTitle + "',ParentCategory='" + categorObj.ParentCategory + "',CategoriesImage='" + savedpath + "',IsActive='" + categorObj.IsActive + "' where categoriesid='"+ categorObj.CategoriesId + "';", conn);
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

        public IActionResult Delete(String CategoriesId)
        {
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {

                conn.Open();  // Open the connection  
                SqlCommand cmd = new SqlCommand("select * from categories where categoriesId='" + CategoriesId + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                 cmd = new SqlCommand("delete from categories where categoriesId='" + CategoriesId + "'", conn);
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(dt.Rows[0]["CategoriesImage"].ToString()) && System.IO.File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, dt.Rows[0]["CategoriesImage"].ToString())))
                {
                    System.IO.File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, dt.Rows[0]["CategoriesImage"].ToString()));
                }
                conn.Close();
                HttpContext.Session.SetString("succssmsg", "Deleted Successfully");


            }
            catch (Exception ex)
            {
                conn.Close();
                HttpContext.Session.SetString("errmsg", "Login Failed" + ex.Message);
            }
            return RedirectToAction("Listing", "Categories");
        }
    }
}
