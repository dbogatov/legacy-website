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
		public MandelbrotModel GetNew(MandelbrotModel model)
		{
            Mandelbrot instance = Mandelbrot.GetNew(model.centerX, model.centerY, model.width, model.height, model.log2scale);
            if (instance == null) return null;

            return new MandelbrotModel
            {
                id = instance.id,
                centerX = instance.centerX,
                centerY = instance.centerY,
                width = instance.width,
                height = instance.height,
                log2scale = instance.log2scale
            };
		}

        // GET: api/Projects/Mandelbrot
        [HttpGet]
        public string GetData(MandelbrotModel model)
        {
            return Mandelbrot.GetData(model.id);
        }
    }
}