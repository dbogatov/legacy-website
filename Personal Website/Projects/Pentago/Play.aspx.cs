using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Personal_Website.Projects.Pentago {
	public partial class Play : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			/*
			if (Request.Cookies["PentagoCookie"] == null) {
				Response.Redirect("Default.aspx");
			} else {
				var cookie = Request.Cookies["PentagoCookie"];
				var context = new PentagoDataContext();
				if (!(
					cookie["Role"].Equals("Host") &&
					context.PentagoGames.Any(g => g.GameCode.Equals(cookie["Code"]) && g.HostKey.Equals(cookie["Key"]))
					||
					cookie["Role"].Equals("Join") &&
					context.PentagoGames.Any(g => g.GameCode.Equals(cookie["Code"]) && g.JoinKey.Equals(cookie["Key"]))
					)) {
					Response.Redirect("Default.aspx");
				}
			}*/
		}
	}
}