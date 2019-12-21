using MVC_Store.Models.Cart;
using MVC_Store.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();

            if (cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "Your cart is empty";
                return View();
            }

            decimal totalPriceCart = 0m;

            foreach (var item in cart)
            {
                totalPriceCart += item.TotalPrice;
            }

            ViewBag.TotalPriceCart = totalPriceCart;
            return View(cart);
        }

        public PartialViewResult CartPartial()
        {
            CartViewModel cartViewModel = new CartViewModel();

            int quantity = 0;
            decimal price = 0m;

            if (Session["cart"] != null)
            {
                List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();

                foreach (var item in cart)
                {
                    quantity += item.Quantity;
                    price += item.Quantity * item.Price;
                }
                cartViewModel.Quantity = quantity;
                cartViewModel.Price = price;
            }
            else
            {
                cartViewModel.Quantity = 0;
                cartViewModel.Price = 0m;
            }
            return PartialView("_CartPartial", cartViewModel);
        }

        public ActionResult AddToCartPartial(int id)
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();

            CartViewModel cartViewModel = new CartViewModel();

            using (Db db = new Db())
            {
                ProductDTO productDTO = db.Products.Find(id);
                CartViewModel productInCart = cart.FirstOrDefault(x => x.ProductId == id);

                if (productInCart == null)
                {
                    cart.Add(new CartViewModel()
                    {
                        ProductId = productDTO.Id,
                        ProductName = productDTO.Name,
                        Quantity = 1,
                        Price = productDTO.Price,
                        Image = productDTO.ImageName
                    });
                }
                else
                {
                    productInCart.Quantity++;
                }
            }
            int quantity = 0;
            decimal price = 0m;

            foreach (var item in cart)
            {
                quantity += item.Quantity;
                price += item.Quantity * item.Price;
            }

            cartViewModel.Quantity = quantity;
            cartViewModel.Price = price;

            Session["cart"] = cart;

            return PartialView("_AddToCartPartial", cartViewModel);
        }

        public JsonResult IncrementProduct(int productId)
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();
            CartViewModel cartViewModel = cart.FirstOrDefault(x => x.ProductId == productId);

            cartViewModel.Quantity++;

            var result = new { qty = cartViewModel.Quantity, price = cartViewModel.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DecrementProduct(int productId)
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();
            CartViewModel cartViewModel = cart.FirstOrDefault(x => x.ProductId == productId);

            cartViewModel.Quantity--;

            var result = new { qty = cartViewModel.Quantity, price = cartViewModel.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}