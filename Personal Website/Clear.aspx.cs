using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Personal_Website.Models;

namespace Personal_Website {
	public partial class Clear : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (Authentication.grantAccess()) {
				Authentication.clearCookie();
			} else {
				Response.Redirect("~/");
			}
		}
	}
}