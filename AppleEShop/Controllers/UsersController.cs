using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppleEShop.Data;
using AppleEShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AppleEShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppleEShopContext _context;

        public UsersController(AppleEShopContext context)
        {
            _context = context;
        }
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(User model)
		{
			if (ModelState.IsValid)
			{
				// Check if user already exists
				var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);
				if (existingUser != null)
				{
					ModelState.AddModelError("", "User already exists with this username or email.");
					return View(model);
				}
				//Role mặc định
				model.Role = "User";
				model.DistrictId = 602;
				model.WardId = 9564;
				model.ProvinceId = 52;

				// Save the new user to the database
				var newUser = new User
				{
					Username = model.Username,
					Email = model.Email,
					Password = Helper.ToMD5(model.Password),

					Role = model.Role, // Set the role explicitly
					DistrictId = model.DistrictId, // Set DistrictId explicitly
					WardId = model.WardId, // Set WardId explicitly
					ProvinceId = model.ProvinceId // Set ProvinceId explicitly
				};
				_context.Add(newUser);
				await _context.SaveChangesAsync();

				// Redirect to login or another appropriate action
				return RedirectToAction("Login");
			}
			return View(model);
		}
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync();
			return RedirectToAction("Login", "Users");
		}
		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			var passHash = Helper.ToMD5(password);
			var user = _context.User.Where(u => u.Username == username && u.Password == passHash).FirstOrDefault<User>();
			if (user == null || _context.User == null)
			{
				ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác!";
				return View();
			}
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username, user.Id.ToString()),
			};
			// Check if user's Role is not null before creating a Claim for it
			if (!string.IsNullOrEmpty(user.Role))
			{
				claims.Add(new Claim(ClaimTypes.Role, user.Role));
			}
			var claimsIdentity = new ClaimsIdentity(
			claims, CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity));
			return RedirectToAction("Index", "Home");
		}
	}
}
