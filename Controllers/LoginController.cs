using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Login.cshtml");
        }

        public IActionResult AdminLogin(String Username = "", String Password = "")
        {
            //string connetionString = "Data Source=ABCE;Initial Catalog=rafilearningmvcdb;Integrated Security=True;MultipleActiveResultSets=True";
            string connetionString = "Data Source=KARTHICES-HOME;Initial Catalog=rafilearningmvcdb;Integrated Security=True;MultipleActiveResultSets=True";

            //string connetionString = "Data Source=KARTHICES-HOME;Initial Catalog=EcommerceDev;Integrated Security=True;MultipleActiveResultSets=True";
        SqlConnection conn = new SqlConnection(connetionString);
            try
            {

            conn.Open();  // Open the connection  
                SqlCommand cmd = new SqlCommand("select * from Users where Username='" + Username + "' and Password='" + Password + "';", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    if(Convert.ToBoolean(dt.Rows[0]["IsActive"]))
                    {
                        HttpContext.Session.SetString("username", dt.Rows[0]["DisplayName"].ToString());
                        HttpContext.Session.SetString("userid", dt.Rows[0]["UserId"].ToString());
                        return RedirectToAction("Sales", "Dashboard");
                    }
                    else
                    {
                        ViewBag.errmsg = "Inactive User. Contact Admin";
                        return View("~/Views/Login.cshtml");
                    }
                }
                else
                {
                    ViewBag.errmsg = "Invalid Username/password";
                    return View("~/Views/Login.cshtml");
                }
                
            }catch(Exception ex)
            {
                ViewBag.errmsg = "Login Failed"+ex.Message;
                return View("~/Views/Login.cshtml");
                conn.Close();
            }
        }

        public IActionResult Logout()
        {
            // destroy the session
            // redirect to login page
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("userid");
            return RedirectToAction("Index", "Login");
        }
    }
}
