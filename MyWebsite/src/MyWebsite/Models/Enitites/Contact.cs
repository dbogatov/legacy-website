using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites {

[Table("Users")]
	public class Contact : AbsEntity {
		[Key]
		[Column("userID")]
		public int Id { get; set; }
		[Column("userName")]
		public string Name { get; set; }
		[Column("userEmail")]
		public string Email { get; set; }
		[Column("userComment")]
		public string Comment { get; set; }
		[Column("userLanguage")]
		public string Language { get; set; }
		public DateTime RegTime { get; set; }
		public string Hash { get; set; }
	}
}