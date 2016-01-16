using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites {

[Table("Feedback")]
	public class Feedback : AbsEntity {
		[Key]
		public int Id { get; set; }
		public string Email { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Url { get; set; }
	}
}