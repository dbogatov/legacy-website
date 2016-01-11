using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models.Repos {

	public interface ICoursesRepo : IAbsRepo<Course> {
		IEnumerable<Course> GetCoursesWithRequirements();
	}

	public class CoursesRepo : AbsRepo<Course>, ICoursesRepo {

		private readonly IAbsRepo<Requirement> _requirements;

		public CoursesRepo(DbContext db, IAbsRepo<Requirement> requirements) : base(db) {

            _requirements = requirements;
        }

		public IEnumerable<Course> GetCoursesWithRequirements() {
            var courses = base.GetItems().ToList();
            var requirements = _requirements.GetItems().ToList();

            courses.ForEach(c => c.Requirement = requirements.FirstOrDefault(r => r.ReqId == c.ReqId));
			
			return courses.ToList();
        }
	}
}
