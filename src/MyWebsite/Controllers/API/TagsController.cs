using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/Tags")]
	public class TagsController : Controller
	{
		private readonly DataContext _context;

		public TagsController(DataContext context)
		{
			_context = context;
		}

		// GET: api/Tags
		[HttpGet]
		public IEnumerable<Tag> GetTags()
		{
			return _context.Tags.AsEnumerable();
		}

		// GET: api/Tags/5
		[HttpGet("{id}", Name = "GetTag")]
		public IActionResult GetTag([FromRoute] int id)
		{

			var tag = _context.Tags.FirstOrDefault(t => t.TagId == id);

			return tag == null ? (IActionResult)HttpNotFound() : Ok(tag);
		}
	}
}