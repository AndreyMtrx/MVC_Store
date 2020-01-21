using MVC_Store.Models.Data;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_Store
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        // Метод обработки запросов аутентификации
        protected void Application_AuthenticateRequest()
        {
            if (User == null)
                return;

            string userName = Context.User.Identity.Name;
            string[] roles = null;

            using(Db db = new Db())
            {
                UserDTO userDTO = db.Users.FirstOrDefault(x => x.UserName == userName);

                if (userDTO == null)
                    return;

                roles = db.UserRoles.Where(x => x.UserId == userDTO.Id).Select(x=>x.Role.Name).ToArray();
            }

            IIdentity userIdentity = new GenericIdentity(userName);
            IPrincipal newUserObject = new GenericPrincipal(userIdentity,roles);

            Context.User = newUserObject;
        }
    }
}