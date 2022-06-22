using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPlus.Ecommerce.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        // This Action Method executes Index.cshtml file under ~/Views/Contact folder and returns that View

        public ActionResult Index()
        {
            return View();
        }
    }
}