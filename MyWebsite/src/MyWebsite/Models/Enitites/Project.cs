using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Models.Enitites {

	[Table("Projects")]
	public class Project : AbsEntity {
		[Key]
		public int ProjectId { get; set; }
		public string Title { get; set; }
		public string DescriptionText { get; set; }
		public DateTime DateCompleted { get; set; }
		public string Weblink { get; set; }
		public string SourceLink { get; set; }
		public string ImgeFilePath { get; set; }

		[NotMapped]
		public virtual IEnumerable<Tag> Tags { get; set; }
	}
}
