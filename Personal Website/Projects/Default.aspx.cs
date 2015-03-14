using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Personal_Website.Projects {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			foreach (var project in new ProjectsDataContext().Projects) {
				var q = new ProjectsDataContext().GetTagsForProject(project.projectID);

			}

			
		}
	}
}