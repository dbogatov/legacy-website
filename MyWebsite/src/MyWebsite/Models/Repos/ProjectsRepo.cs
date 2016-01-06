using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models.Repos {

	public interface IProjectsRepo : IAbsRepo<Project> {
		IEnumerable<Project> GetProjectsWithTags();
	}

	public class ProjectsRepo : AbsRepo<Project>, IProjectsRepo {
		private readonly IAbsRepo<Tag> _tags;
		private readonly IAbsRepo<ProjectTag> _projTags;

		public ProjectsRepo(DbContext db, IAbsRepo<Tag> tags, IAbsRepo<ProjectTag> projTags) : base(db) {
			_tags = tags;
			_projTags = projTags;
		}

		public IEnumerable<Project> GetProjectsWithTags() {
			var projects = base.GetItems();
			var tags = _tags.GetItems();
			var projTags = _projTags.GetItems();

			var result =
				from p in projects
				join pt in projTags on p.ProjectId equals pt.ProjectId into ps
				select new Project {
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
						).AsEnumerable()
				};

			return result.ToList();
		}
	}
}
