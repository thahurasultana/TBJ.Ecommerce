using System.Web.Mvc;

namespace TBJ.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // This Action Method executes Index.cshtml file under ~/Views/Home folder and retruns that View

        public ActionResult Index()
        {
            return View();
        }
    }
}