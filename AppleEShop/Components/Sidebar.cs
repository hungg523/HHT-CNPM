using AppleEShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace AppleEShop.Models
{
    public class Sidebar : ViewComponent
    {
        private readonly AppleEShopContext _context;

        public Sidebar(AppleEShopContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
