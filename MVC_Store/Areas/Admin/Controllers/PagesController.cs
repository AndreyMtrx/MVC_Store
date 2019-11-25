using MVC_Store.Models.Data;
using MVC_Store.Models.ViewModels.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Store.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Обьявляем список для представления
            List<PageViewModel> pageList;
            //Инициализируем список базы данных
            using (Db db = new Db())
            {
                pageList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageViewModel(x)).ToList();
            }
            return View(pageList);
        }

        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // Post: Admin/Pages/AddPage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPage(PageViewModel pageViewModel)
        {
            //Проверка модели на валидность
            if (!ModelState.IsValid)
            {
                return View(pageViewModel);
            }
            using (Db db = new Db())
            {
                //Объявляем переменную для краткого описания(slug)
                string slug;

                //Инициализируем класс PageDTO, устанавлием значение нужным полям
                PagesDTO dto = new PagesDTO();
                dto.Title = pageViewModel.Title.ToUpper();

                if (string.IsNullOrWhiteSpace(pageViewModel.Slug))
                {
                    slug = pageViewModel.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = pageViewModel.Slug.Replace(" ", "-").ToLower();
                }

                //Убеждаемся что заголовок и краткое описание - уникальны
                if (db.Pages.Any(x => x.Title == pageViewModel.Title))
                {
                    ModelState.AddModelError("", "That title already exist.");
                    return View(pageViewModel);
                }

                if (db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That slug already exist");
                    return View(pageViewModel);
                }
                dto.Slug = slug;
                dto.Body = pageViewModel.Body;
                dto.HasSidebar = pageViewModel.HasSidebar;
                dto.Sorting = 100;

                //Сохраняем модель в базу данных
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            //Передаем сообщение через tempdata
            TempData["SM"] = "You have added a new page";

            return RedirectToAction("Index");
        }
    }
}