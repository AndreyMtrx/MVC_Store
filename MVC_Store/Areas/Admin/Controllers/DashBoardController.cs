using System.Web.Mvc;

namespace MVC_Store.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}