using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites {

	[Table("Tags")]
	public class Tag : AbsEntity {
		[Key]
		public int TagId { get; set; }
		public string TagName { get; set; }
	}
}
