using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers {
	public class ProjectController : Controller {

		public IActionResult Index() {
			return RedirectToActionPermanent("Index", "Home");
		}

		public IActionResult Banker() {
			return View();
		}
		
		public IActionResult WPICalendar() {
			return View();
		}
		
		public IActionResult Minesweeper() {
            return View();
        }
		
		public IActionResult Mandelbrot() {
            return View();
        }
		
		public IActionResult Error() {
			return View();
		}
	}
}
