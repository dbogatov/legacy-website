using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Controllers.API
{

    [Produces("application/json")]
    [Route("api/Courses")]
    public class CoursesController : Controller
    {
        private readonly DataContext _context;

        public CoursesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses.Include(c => c.Requirement.ParentRequirement).AsEnumerable();
        }

        // GET: api/Courses/Microeconomics
        [HttpGet("{title}", Name = "GetCourse")]
        public IActionResult GetCourse([FromRoute] string title)
        {

            var course = _context.Courses.FirstOrDefault(c => c.Title.Equals(title));

            return course == null ? (IActionResult)HttpNotFound() : Ok(course);
        }
    }
}