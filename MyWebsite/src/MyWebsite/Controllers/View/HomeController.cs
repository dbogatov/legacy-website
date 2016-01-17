﻿using System;
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
			return View();
		}

		public IActionResult Courses() {
			return View();
		}
		
		public IActionResult Author() {
			return View();
		}

		public IActionResult About() {
			return View();
		}

		public IActionResult Contact() {
			return View();
		}

		public IActionResult Error() {
			return View();
		}
	}
}
