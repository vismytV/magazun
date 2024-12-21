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
			TempData["error1"] = "";
			TempData["role"] = "";
			var rez = _database.GetProduct(); // Получаем список продуктов
			return View(rez); // Передаем продукты в представление

		}


		//початкова сторінка (юзер)
		[Route("Product/Index_product")]
		[HttpPost ]
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

		//історія юзера
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

		//додаэмо новий ордер
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
				return Ok("Order added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error: {ex.Message}");
			}
		}

		//[HttpPost("Index_product")]
		/*public IActionResult Index_product1([FromBody] string firstName, [FromBody] string lastName, [FromBody] List<int> masiv_product)
		{
			var Customer = _database.GetCustomer().FirstOrDefault(c => c.LastName == lastName
				&& c.FirstName == firstName);

			*//*var newOrder = new Order
			{
				CustomerId = Customer.CustomerId,
				OrderDate = DateTime.Now,
				TotalAmount = 1500, // Общая сумма заказа
				OrderProducts = masiv_product.Select(productId => new OrderProduct { ProductId = productId }).ToList()

			};*//*


			//_database.AddOrder(newOrder);

			//return Ok($"Received {masiv_product.Count} products for {firstName} {lastName}");
			return Ok();
		}*/

		[Route("Product/Index_product1")]
		[HttpPost]
		public IActionResult Index_product1([FromForm] string firstName, [FromForm] string lastName, [FromForm] List<int> masiv_product)
		{
			var customer = _database.GetCustomer().FirstOrDefault(c => c.LastName == lastName && c.FirstName == firstName);

			if (customer == null)
			{
				return BadRequest("Customer not found");
			}

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

			return Ok($"Order created for {firstName} {lastName} with {masiv_product.Count} products.");
		}






	}

}