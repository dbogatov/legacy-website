using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Personal_Website.Projects {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

		}

		protected string GetProjects() {
			Personal_Website.Models.DatabaseReset.ResetProjects();
			
			StringBuilder html = new StringBuilder();

			foreach (var project in new ProjectsDataContext().Projects) {

				StringBuilder tags = new StringBuilder();
				foreach (var tag in new ProjectsDataContext().GetTagsForProject(project.projectID)) {
					tags.Append("project-"+tag.tagName.ToString().ToLower()+" ");	
				}

				html.Append(String.Format(
					"<div class='col-sm-6 col-md-4 project-thumbnail {0}' style='padding-top:10px'>" +
					"<div class='thumbnail'>" +
					"<img src='{1}' alt='Here should have been image'>" +
					"<div class='caption'>" +
					"<h3>{2}</h3>" +
					"<p>{3}</p>" +
					"<p><a href='#' class='btn btn-primary' role='button'>Try it!</a> <a href='#' class='btn btn-default' role='button'>View source</a></p>" +
					"</div>" +
					"</div>" +
					"</div>",
					tags, project.imgeFilePath.ToString(), project.title.ToString(), project.descriptionText.ToString()
					));
			}

			return html.ToString();
		}
	}
}