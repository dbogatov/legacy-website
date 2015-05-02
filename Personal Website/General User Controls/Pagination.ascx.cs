using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Personal_Website.General_User_Controls {
	public partial class Pagination : System.Web.UI.UserControl {

		public string URL;
		public int pageNum;
		public int displayNum;
		public int defaultActive;

		protected void Page_Load(object sender, EventArgs e) {
			urlLabel.InnerText = URL;
			numLabel.InnerText = pageNum.ToString();
			actLabel.InnerText = defaultActive.ToString();
			dispLabel.InnerText = displayNum.ToString();
		}
	}
}