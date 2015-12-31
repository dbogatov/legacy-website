using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models.Repos {

	public interface IProjectsRepo {
		IEnumerable<Tags> GetTags();
		Tags GetTag(int tagId);
		Tags AddTag(Tags tags);
		bool DeleteTag(int tagId);
	}

	public class ProjectsRepo : IProjectsRepo {
		private readonly ProjectsDbContext _db;

		public ProjectsRepo(ProjectsDbContext db) {
			_db = db;
		}

		public IEnumerable<Tags> GetTags() {
			return _db.Tags.AsEnumerable();
		}

		public Tags GetTag(int tagId) {
			return _db.Tags.FirstOrDefault(t => t.TagId == tagId);
		}

		public Tags AddTag(Tags tags) {
			_db.Tags.Add(tags);

			return _db.SaveChanges() > 0 ? tags : null;
		}

		public bool DeleteTag(int tagId) {
			var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
			if (tag == null) return false;
			_db.Tags.Remove(tag);
			return _db.SaveChanges() > 0;
		}
	}
}
