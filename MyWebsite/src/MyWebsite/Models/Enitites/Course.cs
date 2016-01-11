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

		[NotMapped]
        public Requirement Requirement { get; set; }
	}
}