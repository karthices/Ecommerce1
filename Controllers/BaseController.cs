using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class BaseController : Controller
    {
        public string connetionString = "Data Source=KARTHICES-HOME;Initial Catalog=rafilearningmvcdb;Integrated Security=True;MultipleActiveResultSets=True";

        //public string connetionString = "Data Source=ABC;Initial Catalog=rafilearningmvcdb;Integrated Security=True;MultipleActiveResultSets=True";

        //public string connetionString = "Data Source=SMARTDB2; Initial Catalog=rafilearningmvcdb; User ID=sa; Password=x911x;";
        //public virtual void Delete(string Id)
        //{

        //}

        // Start AdminMenu 
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {
                string lang = "EN";
                SqlCommand cmd = new SqlCommand("select AdminMenuId," + (lang == "EN" ? " AdminMenuTitle1 as Title" : "AdminMenuTitle2 as Title") + ",AdminMenuParent,AdminMenuFavIcon, AdminMenuController, AdminMenuAction from AdminMenus where AdminMenuStatus=1", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewBag.AdminMenus = dt;

                if (HttpContext.Session.GetString("RandomCustomer") != null)
                {
                    string RandomCustomer = HttpContext.Session.GetString("RandomCustomer");
                    cmd = new SqlCommand("select sum(quantity) as count from cart where isprocessed=0 and randomcustomer='" + RandomCustomer + "'", conn);
                    dt = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        ViewBag.cartcount = Convert.ToInt32(dt.Rows[0]["count"]);
                }



            }
            catch (Exception ex)
            {
                ViewBag.errmsg = "Login Failed" + ex.Message;
            }

        }

        // End AdminMenu 
    }

}
