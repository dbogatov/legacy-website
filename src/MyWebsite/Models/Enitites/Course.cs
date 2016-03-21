using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites
{
	public class Course
	{
		[Key]
		public string Title { get; set; }
		public string Term { get; set; }
		public int Year { get; set; }
		public string CourseId { get; set; }
		public double GradePercent { get; set; }
		public string GradeLetter { get; set; }
		public string Status { get; set; }
		public int ReqId { get; set; }

		public Requirement Requirement { get; set; }
	}

	public class Requirement
	{
		[Key]
		public int ReqId { get; set; }
		public string ReqName { get; set; }

		public int ParentReqId { get; set; }

		public ParentRequirement ParentRequirement { get; set; }

		[NotMapped]
		public ICollection<Course> Courses { get; set; }
	}

	public class ParentRequirement
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }

		public int DisplayColumn { get; set; }

		[NotMapped]
		public ICollection<Requirement> Requirements { get; set; }
	}

}