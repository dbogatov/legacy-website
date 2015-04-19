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
		
			foreach (var project in new ProjectsDataContext().Projects) {

				StringBuilder tags = new StringBuilder();
				foreach (var tag in new ProjectsDataContext().GetTagsForProject(project.projectID)) {
					tags.Append("project-" + tag.tagName.ToString().ToLower() + " ");
				}

				ProjectThubnail control = (ProjectThubnail)LoadControl("~/Projects/ProjectThubnail.ascx");
				control.ProjectType = tags.ToString();
				control.ImageSrc = project.imgeFilePath;
				control.Title = project.title;
				control.Description = project.descriptionText;
				control.TryRef = project.weblink;
				control.SrcRef = project.sourceLink;
				control.Date = String.Format("{0:MMMM, yyyy}", project.dateCompleted);
				projectsDiv.Controls.Add(control);

			}

		}
	}
}