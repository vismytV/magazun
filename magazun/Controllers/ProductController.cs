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

		//початкова сторінка(гість)
		public IActionResult Index_product()
		{
			//if (TempData["error1"] == null)
			//{
				TempData["error1"] = "";
				TempData["role"] = "";
			//}
			
			var rez = _database.GetProduct(); // Получаем список продуктов
			return View(rez); // Передаем продукты в представление

		}

		[Route("Product/Index_product2")]//реєстрація
		[HttpPost]
		public IActionResult Index_product2(string login_user2, string pasw2, 
			string lastName2,string firstName2,string cop_email)//реєстрація
		{
			var rez = _database.GetLogin().FirstOrDefault(l => l.UserLogin == login_user2);
			if (rez != null)
			{
				TempData["error1"] = "Логін " + login_user2 + " вже зайнятий";
				return RedirectToAction("Index_product", "Product");
			}

			var rez3=_database.GetCustomer().FirstOrDefault(c=>c.Email==cop_email);
			if (rez3 != null)
			{
				TempData["error1"] = "Email " + cop_email + " вже зареєстрована";
				return RedirectToAction("Index_product", "Product");
			}

			var newCustomer = new Customer
			{
				FirstName = lastName2,
				LastName = firstName2,
				Email = cop_email
				
			};

			_database.AddCustomer(newCustomer);

			var cust=_database.GetCustomer().FirstOrDefault(c=>c.Email==cop_email);

			var newLogin = new Login
			{
				UserLogin = login_user2,
				Password = pasw2,
				Role = "user",
				idCustomer = cust.CustomerId,
				customer= cust
			};


			_database.AddLogin(newLogin);

			TempData["role"] = "user";
			TempData["error1"] = "Успішно зареэстровано";
			TempData["lastName"] = lastName2;
			TempData["firstName"] = firstName2;
			TempData["login_user"] = login_user2;
			TempData["pasw"] = pasw2;
			TempData["email"] = cop_email;

			return RedirectToAction("Index_product", "Product");
		}

		//початкова сторінка (юзер)
		[Route("Product/Index_product")]
		[HttpPost ]
		public IActionResult Index_product(string login_user, string pasw)//вхід
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
				TempData["email"] = tempo.Email;
			}

			return View(rez1);
		}

		//історія юзера
		[HttpPost]
		public IActionResult Histori(string lastName1, string firstName1, string role1,string login_user1,string pasw1)
		{
			TempData["lastName"] = lastName1;
			TempData["firstName"] = firstName1;
			TempData["error1"] = "";
			TempData["role"] = role1;
			TempData["login_user"] = login_user1;
			TempData["pasw"] = pasw1;

			var login=_database.GetLogin().FirstOrDefault(l=>l.UserLogin== login_user1);

			
			var customer=_database.GetCustomer()
				.FirstOrDefault(c=>c.CustomerId==login.idCustomer);
			

			// Отримуємо всі ордери для знайденого користувача
			var orders = _database.GetOrder()
				.Include(o => o.OrderProducts) // Завантажуємо пов’язані OrderProducts
				.ThenInclude(op => op.Product) // Завантажуємо пов’язані продукти
				.Where(z => z.CustomerId == customer.CustomerId) // Фільтруємо за CustomerId
				.ToList();

			//int a;
			ViewBag.Orders = orders;
			//a=orders.Count();

			/*var p = _database.GetOrder().FirstOrDefault(o => o.CustomerId == customer.CustomerId);
			if (p != null)
			{
				int a1 = 0;
			}*/
			return View();
		}

		//додаэмо новий ордер+++++++++++++++++++
		[HttpPost]
		public IActionResult AddOrder(int customerId, List<int> productIds)
		{
			
			if (productIds == null || !productIds.Any())
				return BadRequest("No products specified for the order.");

			try
			{
				var newOrder = new Order
				{
					CustomerId = customerId,
					OrderDate = DateTime.Now,
					TotalAmount = productIds.Sum(productId =>
						_database.GetProduct().FirstOrDefault(p => p.ProductId == productId)?.Price ?? 0), // Считаем общую сумму
					OrderProducts = productIds.Select(productId => new OrderProduct { ProductId = productId }).ToList()
				};

				_database.AddOrder(newOrder);
				//return Ok("Order added successfully.");
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest($"Error: {ex.Message}");
			}
		}

		
		//приймаємо дані для нового ордеру
		[Route("Product/Index_product1")]
		[HttpPost]
		public IActionResult Index_product1([FromForm] string firstName, [FromForm] string lastName, [FromForm] List<int> masiv_product
			,[FromForm] string role,string login_user1)
		{
			var customer = _database.GetCustomer().FirstOrDefault(c => c.LastName == lastName && c.FirstName == firstName);

			var productPrices = _database.GetProduct() // Получаем список всех продуктов из базы данных
			.Where(p => masiv_product.Contains(p.ProductId)) // Выбираем продукты, которые есть в списке masiv_product
			.Sum(p => p.Price); // Суммируем их цены

			var newOrder = new Order
			{
				CustomerId = customer.CustomerId,
				OrderDate = DateTime.Now,
				TotalAmount = productPrices,
				OrderProducts = masiv_product.Select(productId => new OrderProduct { ProductId = productId }).ToList()
			};

			_database.AddOrder(newOrder);

			TempData["lastName"] = lastName;
			TempData["firstName"] = firstName;
			TempData["error1"] = "";
			TempData["role"] = role;
			TempData["login_user"] = login_user1;


			return Ok($"Order created for {firstName} {lastName} with {masiv_product.Count} products.");
		}

		//додаэмо новий ордер------------------------

		//редагування даних юзера
		[Route("Product/EditProfilUser")]
		[HttpPost]
		public IActionResult EditProfilUser(string info_pasw, string info_login,
			string info_lastName1,string info_firstName1, string info_email2)
		{
			var login=_database.GetLogin().FirstOrDefault(l=>l.UserLogin== info_login);
			if (login == null)
			{
				int qqq = 0;
			}
			var updatedCustomer = new Customer
			{
				CustomerId = login.idCustomer, // Идентификатор клиента
				FirstName = info_firstName1,
				LastName = info_lastName1,
				Email = info_email2
			};

			_database.UpdateCustomer(updatedCustomer);

			var newLogin = new Login
			{
				Id=login.Id,
				UserLogin = info_login,
				Password = info_pasw,
				Role = "user",
				idCustomer = login.idCustomer,
				customer = updatedCustomer
			};


			_database.UpdateLogin(newLogin);

			TempData["role"] = "user";
			TempData["error1"] = "Дані успішно змінено";
			TempData["lastName"] = info_lastName1;
			TempData["firstName"] = info_firstName1;
			TempData["login_user"] = login.UserLogin;
			TempData["pasw"] = info_pasw;
			TempData["email"] = info_email2;

			return RedirectToAction("Index_product", "Product");
			
		}
	}



}