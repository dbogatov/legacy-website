using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers.API {
	[Produces("application/json")]
	[Route("api/Tags")]
	public class TagsController : Controller {
		private readonly IAbsRepo<Tag> _tags;

		public TagsController(IAbsRepo<Tag> tags) {
			_tags = tags;
		}

		// GET: api/Tags
		[HttpGet]
		public IEnumerable<Tag> GetTags() {
			return _tags.GetItems().ToList();
		}

		// GET: api/Tags/5
		[HttpGet("{id}", Name = "GetTag")]
		public IActionResult GetTag([FromRoute] int id) {

			var tag = _tags.GetItem(id);

			return tag == null ? (IActionResult)HttpNotFound() : Ok(tag);
		}
	}
}