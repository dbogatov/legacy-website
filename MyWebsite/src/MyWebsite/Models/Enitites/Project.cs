using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models.Enitites {

	public class Tags {
		[Key]
		public int TagId { get; set; }
		[Required]
		public string TagName { get; set; }
	}
}
