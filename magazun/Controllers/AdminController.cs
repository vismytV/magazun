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
			string login_userA, string paswA,string sort="",string pokaz_spisok_tovar="checked")
		{
			TempData["lastName"] = lastNameA;
			TempData["firstName"] = firstNameA;
			TempData["error1"] = "";
			TempData["role"] = "admin";
			TempData["login_user"] = login_userA;
			TempData["pasw"] = paswA;
			TempData["vubor_sort"] = "";

			if (pokaz_spisok_tovar == "none")
			{
				pokaz_spisok_tovar = "";
			}
			TempData["pokaz_spisok"] = pokaz_spisok_tovar;


			if (vubor_admin == "orders")
			{
				if (sort == "mani")
				{
					TempData["vubor_sort"] = "mani";
					/*var orders = _database.GetOrder()
					.ToList() // Загружаем данные в память
					.OrderBy(o => o.TotalAmount) // Сортируем на стороне клиента
					.ToList();*/


					//макс. сума 1)
					//var orders = _database.GetOrder().OrderByDescending(o => o.TotalAmount).ToList();
					var orders = _database.GetOrder().OrderBy(o => o.TotalAmount).ToList();//(1-min)
					
					ViewBag.Orders = orders;
				}
				else
				{
					//var orders = _database.GetOrder().ToList();//сортировка по дате (1 самая старая)
					var orders = _database.GetOrder().OrderByDescending(
						o => o.OrderDate).ToList();//(1 самая новая)

					ViewBag.Orders = orders;
				}
				
				var customers=_database.GetCustomer().ToList();
				var logins=_database.GetLogin().ToList();
				
				
				ViewBag.Customers = customers;
				ViewBag.Logins = logins;

				return View();
			}
			return View();
		}
	}
}
