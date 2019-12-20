using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Store
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Cart", "cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                new[] { "MVC_Store.Controllers" });

            routes.MapRoute("Shop", "shop/{action}/{name}", new { controller = "Shop", action = "Index", name = UrlParameter.Optional },
                new[] { "MVC_Store.Controllers" });

            routes.MapRoute("Default", "", new { controller = "Shop", action = "Index"},
                new[] { "MVC_Store.Controllers" });
            /*routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );*/
        }
    }
}