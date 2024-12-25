using Microsoft.AspNetCore.Mvc;
using magazun.Data;
using System.Data;
using magazun.Models;
using Microsoft.EntityFrameworkCore;

namespace magazun.Controllers
{
	public class AdminController : Controller
	{
		private readonly IDatabase _database;

		public AdminController(IDatabase database)
		{
			_database = database;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Route("Admin/adminHistori")]
		[HttpPost]
		public IActionResult adminHistori(string vubor_admin, string lastNameA, string firstNameA, string roleA,
			string login_userA, string paswA)
		{
			TempData["lastName"] = lastNameA;
			TempData["firstName"] = firstNameA;
			TempData["error1"] = "";
			TempData["role"] = roleA;
			TempData["login_user"] = login_userA;
			TempData["pasw"] = paswA;

			
			if (vubor_admin == "orders")
			{
				var orders = _database.GetOrder().ToList();
				var customers=_database.GetCustomer().ToList();
				ViewBag.Orders = orders;
				ViewBag.Customers = customers;
				//var a = orders.Count();
				//return RedirectToAction("Histori", "Product");
				return View();
			}
			return View();
		}
	}
}
