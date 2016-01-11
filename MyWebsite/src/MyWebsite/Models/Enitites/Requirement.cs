using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites {

	[Table("DiplomaReqs")]
	public class Requirement : AbsEntity {
		[Key]
		public int ReqId { get; set; }
		public string ReqName { get; set; }
		
		[NotMapped]
		public ICollection<Course> Courses { get; set; }
	}
}
