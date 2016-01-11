using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Controllers.API {
	
	[Produces("application/json")]
	[Route("api/Courses")]
	public class CoursesController : Controller {
		private readonly ICoursesRepo _courses;
		//private readonly IAbsRepo<Requirement> _requirements;

		public CoursesController(ICoursesRepo courses) {
			_courses = courses;
        }

		// GET: api/Courses
		[HttpGet]
		public IEnumerable<Course> GetCourses() {
            return _courses.GetCoursesWithRequirements();
        }
		
		// GET: api/Courses/Microeconomics
		[HttpGet("{title}", Name = "GetCourse")]
		public IActionResult GetCourse([FromRoute] string title) {

            var course = _courses.GetItems().FirstOrDefault(c => c.Title.Equals(title));

            return course == null ? (IActionResult)HttpNotFound() : Ok(course);
		}
	}
}