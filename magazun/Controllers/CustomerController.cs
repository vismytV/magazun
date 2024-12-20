//using magazun.Models;
using magazun.Data;
using Microsoft.AspNetCore.Mvc;

namespace magazun.Controllers
{
	public class CustomerController : Controller
	{
		private readonly IDatabase _database;

		public CustomerController(IDatabase database)
		{
			_database = database;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
