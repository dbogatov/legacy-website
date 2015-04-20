using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Personal_Website.Models;

namespace Personal_Website.Author {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			downloads.Visible = Authentication.grantAccess();
			independentProjects.Visible = Authentication.grantAccess();
			academicProjects.Visible = Authentication.grantAccess();
			experience.Visible = Authentication.grantAccess();
		}
	}
}