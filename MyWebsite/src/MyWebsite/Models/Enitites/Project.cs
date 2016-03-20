using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites
{

	[Table("Projects")]
	public class Project
	{
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

	[Table("ProjectTag")]
	public class ProjectTag
	{
		[Key]
		public int RelId { get; set; }
		public int ProjectId { get; set; }
		public int TagId { get; set; }
	}

	[Table("Tags")]
	public class Tag
	{
		[Key]
		public int TagId { get; set; }
		public string TagName { get; set; }
	}
}
