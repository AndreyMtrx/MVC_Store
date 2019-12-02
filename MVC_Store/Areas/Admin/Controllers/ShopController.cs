using MVC_Store.Models.Data;
using MVC_Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Categories()
        {
            List<CategoryViewModel> categoryVMList;
            using(Db db = new Db())
            {
                categoryVMList = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryViewModel(x)).ToList();
            }
            return View(categoryVMList);
        }
        [HttpPost]
        public string AddNewCategory(string catName)
        {
            string id;
            using(Db db = new Db())
            {
                if (db.Categories.Any(x => x.Name == catName))
                {
                    return "titletaken";
                }

                CategoryDTO categoryDTO = new CategoryDTO();
                categoryDTO.Name = catName;
                categoryDTO.Slug = catName.Replace(" ", "-").ToLower();
                categoryDTO.Sorting = 100;

                db.Categories.Add(categoryDTO);
                db.SaveChanges();

                id = categoryDTO.Id.ToString();
            }
            return id;
        }
        public ActionResult DeleteCategory(int id) {
            using(Db db = new Db())
            {
                CategoryDTO categoryDTO = db.Categories.Find(id);
                if(categoryDTO == null)
                {
                    return Content("This category does not exist");
                }
                db.Categories.Remove(categoryDTO);
                db.SaveChanges();
            }
            TempData["DM"] = "You have deleted the category";
            return RedirectToAction("Categories");
        }
        [HttpPost]
        public string RenameCategory(string newCatName,int id)
        {
            using(Db db = new Db())
            {
                if(db.Categories.Any(x=>x.Name == newCatName))
                {
                    return "titletaken";
                }

                CategoryDTO categoryDTO = db.Categories.Find(id);
                categoryDTO.Name = newCatName;
                categoryDTO.Slug = newCatName.Replace(" ", "-").ToLower();
                db.SaveChanges();

            }
            return "Fine";
        }
    }
}