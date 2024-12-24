using Microsoft.AspNetCore.Mvc;
using magazun.Data;

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

		
		public IActionResult admin_vubor(string vubor_admin)
		{
			return View();
		}
	}
}
