using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Models.Enitites {
	[Table("ProjectTag")]
	public class ProjectTag : AbsEntity {
		[Key]
		public int RelId { get; set; }
		public int ProjectId { get; set; }
		public int TagId { get; set; }
	}
}
