using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers.API {
	[Produces("application/json")]
	[Route("api/Projects")]
	public class ProjectsController : Controller {
		private readonly IProjectsRepo _projects;

		public ProjectsController(IProjectsRepo projects) {
			_projects = projects;

        }

		// GET: api/Projects
		[HttpGet]
		public IEnumerable<Project> GetProjects() {
			return _projects.GetProjectsWithTags();
		}

		// GET: api/Projects/5
		[HttpGet("{id}", Name = "GetProject")]
		public IActionResult GetProject([FromRoute] int id) {

			var project = _projects.GetItem(id);

			return project == null ? (IActionResult)HttpNotFound() : Ok(project);
		}
	}
}