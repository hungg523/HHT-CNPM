using AppleEShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace AppleEShop.Models
{
    public class CategoriesList : ViewComponent
    {
        private readonly AppleEShopContext _context;

        public CategoriesList(AppleEShopContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
