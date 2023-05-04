using Edu_Store.Enums;
using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edu_Store.Controllers
{
    [Authorize( Roles = nameof( Roles.Student ) )]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CartManager cartManager { get; }
        public CartController( CartManager _cartManager , UserManager<ApplicationUser> userManager )
        {
            cartManager = _cartManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index( )
        {

            var user = await userManager.GetUserAsync( User );
            return View( cartManager.GetAll( user.Id ) );
        }

        public IActionResult Create( )
        {
            return View( );
        }

        [HttpPost]
        public IActionResult Create( Cart cart )
        {
            try
            {
                cartManager.Add( cart );
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View( );
            }
        }

        public IActionResult Delete( int id )
        {
            cartManager.DeleteCart( id );
            return RedirectToAction( nameof( Index ) );
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
