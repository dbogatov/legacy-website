using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/Projects")]
	public class ProjectsController : Controller
	{
		private readonly DataContext _context;

		public ProjectsController(DataContext context)
		{
			_context = context;
		}

		// GET: api/Projects
		[HttpGet]
		public IEnumerable<Project> GetProjects()
		{
			// TODO change to joint entity

			var projects = _context.Projects.ToList();
			var tags = _context.Tags.ToList();
			var projTags = _context.ProjectTags.ToList();

			var result =
				from p in projects
				join pt in projTags on p.ProjectId equals pt.ProjectId into ps
				select new Project
				{
					ProjectId = p.ProjectId,
					Title = p.Title,
					DescriptionText = p.DescriptionText,
					SourceLink = p.SourceLink,
					ImgeFilePath = p.ImgeFilePath,
					DateCompleted = p.DateCompleted,
					Weblink = p.Weblink,
					Tags = (
						from t in tags
						join pt in ps on t.TagId equals pt.TagId
						select t
						).ToList()
				};

			return result.OrderByDescending(p => p.DateCompleted).ToList();
		}

		// GET: api/Projects/5
		[HttpGet("{id}", Name = "GetProject")]
		public IActionResult GetProject([FromRoute] int id)
		{
			var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);

			return project == null ? (IActionResult)NotFound() : Ok(project);
		}
	}
}