﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Personal_Website {
	public partial class Notification : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			this.Master.FindControl("signInRequest").Visible = false;
		}
	}
}