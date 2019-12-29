using MVC_Store.Models.Data;
using MVC_Store.Models.ViewModels.Account;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Store.Controllers
{
    public class AccountController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("Login");
        }

        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        [ActionName("create-account")]
        [HttpPost]
        public ActionResult CreateAccount(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return View("CreateAccount", userViewModel);

            if (!userViewModel.Password.Equals(userViewModel.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View("CreateAccount", userViewModel);
            }

            using (Db db = new Db())
            {
                if (db.Users.Any(x => x.UserName.Equals(userViewModel.UserName)))
                {
                    ModelState.AddModelError("", "This user name already exist");
                    userViewModel.UserName = "";
                    return View("CreateAccount", userViewModel);
                }
                UserDTO userDTO = new UserDTO()
                {
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    EmailAddress = userViewModel.EmailAddress,
                    UserName = userViewModel.UserName,
                    Password = userViewModel.Password
                };
                db.Users.Add(userDTO);
                db.SaveChanges();

                int id = userDTO.Id;

                UserRoleDTO userRoleDTO = new UserRoleDTO()
                {
                    UserId = id,
                    RoleId = 2
                };
                db.UserRoles.Add(userRoleDTO);
                db.SaveChanges();
            }

            TempData["SM"] = "You have succesfully registered";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            string userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(userName))
                return RedirectToAction("user-profile");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
                return View(loginUser);

            bool isValid = false;
            using (Db db = new Db())
            {
                if (db.Users.Any(x => x.UserName.Equals(loginUser.UserName) && x.Password.Equals(loginUser.Password)))
                    isValid = true;

                if (!isValid)
                {
                    ModelState.AddModelError("", "Invalid user name or password");
                    return View(loginUser);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(loginUser.UserName, loginUser.RememberMe);
                    return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.UserName, loginUser.RememberMe));
                }
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}