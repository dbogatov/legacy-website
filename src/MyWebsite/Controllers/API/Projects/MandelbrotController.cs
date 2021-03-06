using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models.Mandelbrot;
using MyWebsite.ViewModels.Mandelbrot;
using Newtonsoft.Json;

namespace MyWebsite.Controllers.API.Projects
{

	[Produces("application/json")]
	[Route("api/Projects/Mandelbrot")]
	public class MandelbrotController : Controller
	{
		// GET: api/Projects/Mandelbrot/GetNew
		[HttpGet]
		[Route("GetNew")]
		public MandelbrotModel GetNew(MandelbrotModel model)
		{
            Mandelbrot instance = Mandelbrot.GetNew(model.id, model.centerX, model.centerY, model.width, model.height, model.log2scale);
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

        // GET: api/Projects/Mandelbrot/GetData
        [HttpGet]
		[Route("GetData")]
        public string GetData(int id)
        {
            return JsonConvert.SerializeObject(Mandelbrot.GetData(id));
        }

        // GET: api/Projects/Mandelbrot/IsDone
        [HttpGet]
		[Route("IsDone")]
        public uint? IsDone(int id)
        {
            return Mandelbrot.IsDone(id);
        }
    }
}