using AppleEShop.Data;
using AppleEShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppleEShop.Controllers
{
    public class HomeController : Controller
    {
			private readonly AppleEShopContext _context;

			public HomeController(AppleEShopContext context)
			{
				_context = context;
			}

			public IActionResult Index()
			{
				return View(_context.Product.Include(p => p.Category).ToList());
			}

			public IActionResult Privacy()
			{
				return View();
			}

			public IActionResult AdminHome()
			{
				return View();
			}

			[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
			public IActionResult Error()
			{
				return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			}
		}
	}
