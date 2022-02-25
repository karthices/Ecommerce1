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
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
            //if (HttpContext.Session.GetString("username") == null) RedirectToAction("Index", "Login");
        }
        public IActionResult Index() // sales dashboard
        {
            return View("~/Views/Admin/Sales.cshtml");
        }

        public IActionResult Sales() // sales dashboard
        {
            if (HttpContext.Session.GetString("username") == null) return RedirectToAction("Index", "Login");
            return View("~/Views/Admin/Sales.cshtml");
        }
    }
}
