using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites {

	[Table("SimpleGrades")]
	public class Course : AbsEntity {
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
	
	[Table("DiplomaReqs")]
	public class Requirement : AbsEntity {
		[Key]
		public int ReqId { get; set; }
		public string ReqName { get; set; }
		
		public int parentReqId { get; set; }

		public ParentRequirement ParentRequirement { get; set; }

        [NotMapped]
		public ICollection<Course> Courses { get; set; }
	}
	
	[Table("DiplomaReqParent")]
	public class ParentRequirement : AbsEntity {
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		
		public int DisplayColumn { get; set; }
		
		[NotMapped]
		public ICollection<Requirement> Requirements { get; set; }
	}
	
}