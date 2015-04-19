using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Personal_Website.Models;

namespace Personal_Website.General_User_Controls {
	public partial class SpecializedSignInRequest : System.Web.UI.UserControl {

		public string Text;

		protected void Page_Load(object sender, EventArgs e) {
			alertText.InnerText = Text;

			returnBtn.HRef += "?returnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri;
			alertBanner.Visible = !(Authentication.isRegistered() || Authentication.isDemo());
		}
	}
}