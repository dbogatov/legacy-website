using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Courses()
		{
			return View();
		}

		public IActionResult Author()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
