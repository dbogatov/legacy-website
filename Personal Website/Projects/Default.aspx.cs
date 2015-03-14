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
			//Personal_Website.Models.DatabaseReset.ResetProjects();
			
			StringBuilder html = new StringBuilder();

			foreach (var project in new ProjectsDataContext().Projects) {

				StringBuilder tags = new StringBuilder();
				foreach (var tag in new ProjectsDataContext().GetTagsForProject(project.projectID)) {
					tags.Append("project-"+tag.tagName.ToString().ToLower()+" ");	
				}

				html.Append(String.Format(
					"<div class='col-sm-6 col-md-4 project-thumbnail {0}' style='padding-top:10px' >" +
						"<div class='thumbnail' style='height: 450px'>" +
							"<img src='{1}' alt='Here should have been an image' style='max-height:255px'>" +
							"<div class='fixHeight'></div>" +
							"<div class='caption'>" +
								"<h3>{2}</h3>" +
								"<h5>{6}</h5>" +
								"<p class='description' style='text-align: justify;'>{3}</p>" +
								"<p><a href='{4}' target=_blank class='btn btn-primary' role='button'>Try it!</a> <a href='{5}' target=_blank class='btn btn-default' role='button'>View source</a></p>" +
							"</div>" +
						"</div>" +
					"</div>",
					tags, project.imgeFilePath.ToString(), project.title.ToString(), project.descriptionText.ToString(), project.weblink.ToString(), project.sourceLink.ToString(), String.Format("{0:MMMM, yyyy}", project.dateCompleted)
					));
			}

			return html.ToString();
		}
	}
}