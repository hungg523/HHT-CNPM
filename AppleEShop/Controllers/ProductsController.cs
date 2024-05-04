using AppleEShop.Models.ViewModels;
using AppleEShop.Models;
using Microsoft.AspNetCore.Mvc;
using AppleEShop.Data;
using Microsoft.EntityFrameworkCore;

namespace AppleEShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppleEShopContext _context;
        public int PageSize = 9;

        public ProductsController(AppleEShopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int productPage = 1)
        {
            IQueryable<Product> query = _context.Product.Include(p => p.Category);
            decimal minPrice = 1;
            decimal maxPrice = 9999999;
            if (query.Count() > 0)
            {
                maxPrice = query.Max(x => x.Price);
                minPrice = query.Min(x => x.Price);
            }
            ViewBag.MaxPrice = maxPrice;
            ViewBag.MinPrice = minPrice;
            return View
            (
                new ProductListViewModel
                {
                    Product = _context.Product.Skip((productPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Product.Count()
                    }
                }
             );
        }
    }
}
