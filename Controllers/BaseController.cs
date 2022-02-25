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



            }
            catch (Exception ex)
            {
                ViewBag.errmsg = "Login Failed" + ex.Message;
            }

        }

        // End AdminMenu 
    }

}
