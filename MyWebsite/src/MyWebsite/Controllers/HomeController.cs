using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers {
	public class HomeController : Controller {
		private readonly IProjectsRepo _projectsRepo;

		public HomeController(IProjectsRepo projectsRepo) {
			_projectsRepo = projectsRepo;
		}


		public IActionResult Index() {

			var o = (IProjectsRepo)HttpContext.RequestServices.GetService(typeof(IProjectsRepo));

			var p = o.GetTags();
			var f = p.Count();

			return View();
		}

		public IActionResult About() {
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact() {
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error() {
			return View();
		}
	}
}
