using MVC_Store.Models.Data;
using MVC_Store.Models.ViewModels.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Store.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
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

        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        [HttpPost]
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
                else if (db.Pages.Any(x => x.Slug == slug))
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

        [HttpGet]
        public ActionResult EditPage(int id)
        {
            PageViewModel pageViewModel;
            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);
                if (dto == null)
                {
                    return Content("The page does not exist");
                }
                pageViewModel = new PageViewModel(dto);
            }
            return View(pageViewModel);
        }

        [HttpPost]
        public ActionResult EditPage(PageViewModel pageViewModel)

        {
            if (!ModelState.IsValid)
            {
                return View(pageViewModel);
            }
            using (Db db = new Db())
            {
                int id = pageViewModel.Id;
                string slug = "home";

                PagesDTO dto = db.Pages.Find(id);
                dto.Title = pageViewModel.Title;

                if (pageViewModel.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(pageViewModel.Slug))
                    {
                        slug = pageViewModel.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = pageViewModel.Slug.Replace(" ", "-").ToLower();
                    }
                }

                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == pageViewModel.Title))
                {
                    ModelState.AddModelError("", "That title already exist.");
                    return View(pageViewModel);
                }
                else if (db.Pages.Where(x => x.Id != id).Any(x => x.Slug == pageViewModel.Slug))
                {
                    ModelState.AddModelError("", "That slug already exist.");
                    return View(pageViewModel);
                }

                dto.Slug = slug;
                dto.Body = pageViewModel.Body;
                dto.HasSidebar = pageViewModel.HasSidebar;

                db.SaveChanges();
            }
            TempData["SM"] = "You have edited the page";
            return RedirectToAction("Index");
        }

        public ActionResult PageDetails(int id)
        {
            PageViewModel pageViewModel;
            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);
                if (dto == null)
                {
                    return Content("The page does not exist");
                }
                pageViewModel = new PageViewModel(dto);
            }
            return View(pageViewModel);
        }

        public ActionResult DeletePage(int id)
        {
            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);

                if (dto == null)
                {
                    return Content("That page does not exist");
                }

                db.Pages.Remove(dto);
                db.SaveChanges();
            }

            TempData["DM"] = "You have deleted the page";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditSidebar()
        {
            SidebarViewModel sidebarViewModel;
            using(Db db = new Db())
            {
                SidebarDTO sidebarDTO = db.Sidebars.Find(1);
                sidebarViewModel = new SidebarViewModel(sidebarDTO);
            }
            return View(sidebarViewModel);
        }
        [HttpPost]
        public ActionResult EditSidebar(SidebarViewModel sidebarViewModel)
        {
            using(Db db = new Db())
            {
                SidebarDTO sidebarDTO = db.Sidebars.Find(1);
                sidebarDTO.Body = sidebarViewModel.Body;
                db.SaveChanges();
            }
            TempData["SM"] = "You have edited the sidebar";
            return RedirectToAction("EditSidebar");
        }
    }
}