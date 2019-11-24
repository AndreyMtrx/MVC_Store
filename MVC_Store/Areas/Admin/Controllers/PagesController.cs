using System.Web.Mvc;

namespace MVC_Store.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View();
        }
    }
}