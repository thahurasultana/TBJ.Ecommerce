using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TBJ.Ecommerce.Controllers
{
    public class AboutController : Controller
    {
        // GET: About 
        // This Action Method executes Index.cshtml file under ~/Views/About folder and returns that View
        public ActionResult Index()
        {
            return View();
        }
    }
}