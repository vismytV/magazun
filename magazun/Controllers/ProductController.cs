using Microsoft.AspNetCore.Mvc;
using magazun.Data;
using System.Data;
using magazun.Models;
using Microsoft.EntityFrameworkCore;

namespace magazun.Controllers
{
	public class Product : Controller
	{
		private readonly IDatabase _database;

		public Product(IDatabase database)
		{
			_database = database;
		}

		public IActionResult Index_product()
		{
			TempData["error1"] = "";
			TempData["role"] = "";
			var rez = _database.GetProduct(); // Получаем список продуктов
			return View(rez); // Передаем продукты в представление

		}

		[HttpPost]
		public IActionResult Index_product(string login_user, string pasw)
		{
			var rez = _database.GetLogin().FirstOrDefault(l => l.UserLogin == login_user
			&& l.Password == pasw);

			var rez1 = _database.GetProduct();

			if (rez == null)
			{
				TempData["error1"] = "Невірний логін або пароль";
			}
			else
			{
				var tempo = _database.GetCustomer().FirstOrDefault(c => c.CustomerId == rez.idCustomer);
				TempData["role"] = rez.Role;
				TempData["error1"] = "";
				TempData["lastName"] = tempo.LastName;
				TempData["firstName"] = tempo.FirstName;
				TempData["login_user"] = login_user;
				TempData["pasw"] = pasw;
			}


			return View(rez1);
		}

		[HttpPost]
		public IActionResult Histori(string lastName1, string firstName1, string role1)
		{
			TempData["lastName"] = lastName1;
			TempData["firstName"] = firstName1;
			TempData["error1"] = "";
			TempData["role"] = role1;

			var customer=_database.GetCustomer().FirstOrDefault(c=>c.LastName==lastName1 && firstName1==firstName1);

			// Отримуємо всі ордери для знайденого користувача
			var orders = _database.GetOrder()
				.Include(o => o.OrderProducts) // Завантажуємо пов’язані OrderProducts
				.ThenInclude(op => op.Product) // Завантажуємо пов’язані продукти
				.Where(z => z.CustomerId == customer.CustomerId) // Фільтруємо за CustomerId
				.ToList();

			int a;
			ViewBag.Orders = orders;
				a=orders.Count();

			var p = _database.GetOrder().FirstOrDefault(o => o.CustomerId == customer.CustomerId);
			if (p != null)
			{
				int a1 = 0;
			}
			return View();
		}
	}

}