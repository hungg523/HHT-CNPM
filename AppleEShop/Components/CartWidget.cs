using AppleEShop.Data;
using AppleEShop.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AppleEShop.Models
{
    public class CartWidget : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
