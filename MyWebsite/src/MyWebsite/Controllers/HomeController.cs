using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers {
	public class HomeController : Controller {

		public IActionResult Index() {
			var o = (IAbsRepo<Tag>)HttpContext.RequestServices.GetService(typeof(IAbsRepo<Tag>));

			var p = o.GetItems();
			var f = p.Count();

			var oe = (IAbsRepo<Project>)HttpContext.RequestServices.GetService(typeof(IAbsRepo<Project>));

			var pe = oe.GetItems();
			var fe = pe.Count();

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
