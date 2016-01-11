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
            System.Console.WriteLine("Get courses");
            var t = _courses.GetItems().Count();
			System.Console.WriteLine("Count: " + t);
			
            return _courses.GetCoursesWithRequirements();
        }
		/*
		[HttpGet]
		[Route("Requirements")]
		public IEnumerable<Requirement> GetRequirements() {
            System.Console.WriteLine("Get Requirements");
            var t = _requirements.GetItems().Count();
			System.Console.WriteLine("Count: " + t);
			
            return _requirements.GetItems();
        }*/

		// GET: api/Courses/Microeconomics
		[HttpGet("{title}", Name = "GetCourse")]
		public IActionResult GetCourse([FromRoute] string title) {

            var course = _courses.GetItems().FirstOrDefault(c => c.Title.Equals(title));

            return course == null ? (IActionResult)HttpNotFound() : Ok(course);
		}
	}
}