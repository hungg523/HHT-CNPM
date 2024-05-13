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

        [HttpPost]
        public async Task<IActionResult> Search(string keywords, int productPage = 1)
        {
            return View
            ("Index",
                new ProductListViewModel
                {
                    Product = _context.Product.Where(p => p.Name.Contains(keywords)).Skip((productPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Product.Count()
                    }
                }
             );
        }

        public async Task<IActionResult> Index1()
        {
            IEnumerable<Product> products = _context.Product.Include(p => p.Category); // Lấy danh sách sản phẩm
            return View(products);
        }
        public IActionResult Viewbestsell() //hiển thị danh sách bestseller
        {
            // Giả sử bạn lấy danh sách các sản phẩm bán chạy từ database
            IEnumerable<Product> topSellingProducts = _context.Product.Include(p => p.Category).Include(p => p.IsBestSeller);
            // Lưu danh sách vào ViewBag để sử dụng trong view
            ViewBag.TopSellingProducts = topSellingProducts;
            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id, int? page)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        public async Task<IActionResult> Filter(string category, decimal? priceMin, decimal? priceMax, int productPage = 1)
        {
            var stringCate = Request.Query["category"].ToString();
            ViewBag.Cate = stringCate;
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
            //ViewBag.MaxPriceFilter = (priceMax == null ? maxPrice : priceMax);
            //ViewBag.MinPriceFilter = (priceMin == null ? minPrice : priceMin);
            var model = new ProductListViewModel();
            if (!string.IsNullOrEmpty(stringCate))
            {
                query = query.Where(p => stringCate.Contains(p.Category.Name));
            }
            if (priceMin.HasValue)
            {
                query = query.Where(p => p.Price >= priceMin.Value);
            }

            if (priceMax.HasValue)
            {
                query = query.Where(p => p.Price <= priceMax.Value);
            }
            query = query.Skip((productPage - 1) * PageSize).Take(PageSize);
            var PagingInfo = new PagingInfo
            {
                ItemsPerPage = PageSize,
                CurrentPage = productPage,
                TotalItems = _context.Product.Count()
            };
            model.Product = query;
            model.PagingInfo = PagingInfo;
            return View(model);
        }
    }
}
