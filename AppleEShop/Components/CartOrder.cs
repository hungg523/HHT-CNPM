using AppleEShop.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AppleEShop.Models
{
    public class CartOrder : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
