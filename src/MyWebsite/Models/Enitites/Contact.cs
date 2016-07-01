using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites
{
	[Table("Contact")]
	public class Contact
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Comment { get; set; }
		public string Language { get; set; }
		public DateTime RegTime { get; set; }
		public string Hash { get; set; }
	}
}