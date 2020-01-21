using MVC_Store.Models.Data;
using MVC_Store.Models.ViewModels;
using MVC_Store.Models.ViewModels.Shop;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class ShopController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<ProductViewModel> productVMList;
            using (Db db = new Db())
            {
                productVMList = await Task.Run(() =>
                    db.Products.ToArray().Select(x => new ProductViewModel(x)).ToList());
            }
            return View(productVMList);
        }

        public PartialViewResult CategoryMenuPartial()
        {
            List<CategoryViewModel> categoriesVMList;

            using (Db db = new Db())
            {
                categoriesVMList = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryViewModel(x)).ToList();
            }

            return PartialView("_CategoryMenuPartial", categoriesVMList);
        }

        public ActionResult Category(string name)
        {
            List<ProductViewModel> productVMList;

            using (Db db = new Db())
            {
                CategoryDTO categoryDTO = db.Categories.Where(x => x.Slug == name).FirstOrDefault();
                int categoryId;
                if (categoryDTO == null)
                {
                    return Content("This category does not exist");
                }
                else
                {
                    categoryId = categoryDTO.Id;
                }

                productVMList = db.Products.ToArray().Where(x => x.CategoryId == categoryId).Select(x => new ProductViewModel(x)).ToList();

                ProductDTO productCat = db.Products.Where(x => x.CategoryId == categoryId).FirstOrDefault();
                if (productCat == null)
                {
                    string categoryName = db.Categories.Where(x => x.Slug == name).Select(x => x.Name).FirstOrDefault();
                    ViewBag.CategoryName = categoryName;
                }
                else
                {
                    ViewBag.CategoryName = productCat.CategoryName;
                }
            }

            return View(productVMList);
        }

        [ActionName("product-details")]
        public ActionResult ProductDetails(string name)
        {
            ProductDTO productDTO;
            ProductViewModel productViewModel;

            int id = 0;

            using (Db db = new Db())
            {
                if (!db.Products.Any(x => x.Slug.Equals(name)))
                {
                    return RedirectToAction("Index");
                }

                productDTO = db.Products.Where(x => x.Slug.Equals(name)).FirstOrDefault();
                id = productDTO.Id;

                productViewModel = new ProductViewModel(productDTO);
            }

            string GalleryPath = $"{Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs")}";
            productViewModel.GalleryImages = Directory.EnumerateFiles(GalleryPath).Select(fn => Path.GetFileName(fn));

            return View("ProductDetails", productViewModel);
        }
    }
}