using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Mandelbrot;
using MyWebsite.ViewModels.Mandelbrot;

namespace MyWebsite.Controllers.API.Projects
{

	[Produces("application/json")]
	[Route("api/Projects/Mandelbrot")]
	public class MandelbrotController : Controller
	{

		// GET: api/Projects/Mandelbrot
		[HttpGet]
		public IEnumerable<IEnumerable<int>> GetMandelbrot(MandelbrotViewModel model)
		{
			Mandelbrot mandelbrot = new Mandelbrot(model);

			return mandelbrot.computeMatrix();
		}
	}
}