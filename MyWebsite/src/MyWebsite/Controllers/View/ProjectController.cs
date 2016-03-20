using Microsoft.AspNet.Mvc;

namespace MyWebsite.Controllers
{
	public class ProjectController : Controller
	{

		public IActionResult Index()
		{
			return RedirectToActionPermanent("Index", "Home");
		}

		public IActionResult Banker()
		{
			return View();
		}

		public IActionResult WPICalendar()
		{
			return View();
		}

		public IActionResult Minesweeper()
		{
			return View();
		}

		public IActionResult GoogleFinanceParser()
		{
			return View();
		}

		public IActionResult Mandelbrot()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
