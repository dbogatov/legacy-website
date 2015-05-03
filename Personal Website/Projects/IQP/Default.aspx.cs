using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Personal_Website.Projects.IQP {
	public partial class Default : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {

			Pagination.defaultActive = Request.QueryString["page"] != null ? Int32.Parse(Request.QueryString["page"]) : 1;

			pageLabel.InnerText = Request.QueryString["page"] != null ? Request.QueryString["page"] : "1";
		}
	}
}