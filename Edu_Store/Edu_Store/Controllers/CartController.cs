using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Edu_Store.Controllers
{
    public class CartController : Controller
    {
        public CartManager cartManager { get; }
        public CartController(CartManager _cartManager)
        {
            cartManager = _cartManager;
        }
        public IActionResult Index()
        {
            return View(cartManager.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cart cart)
        {
            try
            {
                cartManager.Add(cart);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            cartManager.DeleteCart(id);
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
