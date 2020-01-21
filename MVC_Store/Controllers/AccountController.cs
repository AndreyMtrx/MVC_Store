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

        [ActionName("user-profile")]
        [HttpGet]
        public ActionResult UserProfile()
        {
            string userName = User.Identity.Name;

            UserProfileViewModel model;

            using (Db db = new Db())
            {
                UserDTO userDTO = db.Users.FirstOrDefault(x => x.UserName == userName);
                model = new UserProfileViewModel(userDTO);
            }

            return View("UserProfile", model);
        }

        [ActionName("user-profile")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserProfileViewModel model)
        {
            bool userNameIsChanged = false;

            if (!ModelState.IsValid)
            {
                return View("UserProfile", model);
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match");
                    return View("UserProfile", model);
                }
            }

            using (Db db = new Db())
            {
                string userName = User.Identity.Name;
                if (userName != model.UserName)
                {
                    userName = model.UserName;
                    userNameIsChanged = true;
                }

                if (db.Users.Where(x => x.Id != model.Id).Any(x => x.UserName == userName))
                {
                    ModelState.AddModelError("", $"User name {model.UserName} already exist");
                    model.UserName = string.Empty;
                    return View("UserProfile", model);
                }

                UserDTO userDTO = db.Users.Find(model.Id);
                userDTO.FirstName = model.FirstName;
                userDTO.LastName = model.LastName;
                userDTO.EmailAddress = model.EmailAddress;
                userDTO.UserName = model.UserName;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    userDTO.Password = model.Password;
                }
                db.SaveChanges();
            }

            TempData["SM"] = "You have successfully edited your pprofile";

            if (!userNameIsChanged)
                return View("UserProfile", model);
            else
                return RedirectToAction("Logout");
        }

        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        [ActionName("create-account")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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

        public PartialViewResult UserNavPartial()
        {
            string userName = User.Identity.Name;

            UserNavPartialViewModel model;

            using (Db db = new Db())
            {
                UserDTO userDTO = db.Users.FirstOrDefault(x => x.UserName == userName);
                model = new UserNavPartialViewModel()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName
                };
            }

            return PartialView("_UserNavPartial", model);
        }
    }
}