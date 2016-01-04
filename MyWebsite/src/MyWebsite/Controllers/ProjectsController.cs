using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers {
	[Produces("application/json")]
	[Route("api/Projects")]
	public class ProjectsController : Controller {
		private readonly IAbsRepo<Project> _projects;

		public ProjectsController(IAbsRepo<Project> projects) {
			_projects = projects;
		}

		// GET: api/Projects
		[HttpGet]
		public IEnumerable<Project> GetProjects() {
			return _projects.GetItems().ToList();
		}

		// GET: api/Projects/5
		[HttpGet("{id}", Name = "GetProject")]
		public IActionResult GetProject([FromRoute] int id) {

			var project = _projects.GetItem(id);

			return project == null ? (IActionResult)HttpNotFound() : Ok(project);
		}
	}
}