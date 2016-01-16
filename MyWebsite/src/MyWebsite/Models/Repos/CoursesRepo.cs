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

        public CoursesRepo(AbsDbContext db) : base(db) {}

		public IEnumerable<Course> GetCoursesWithRequirements() {
            return base.GetItemsWithInclude<ParentRequirement>(c => c.Requirement.ParentRequirement);
        }
	}
}