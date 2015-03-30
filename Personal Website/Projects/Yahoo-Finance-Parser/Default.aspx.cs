using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text.RegularExpressions;

namespace Personal_Website.Projects.Yahoo_Finance_Parser {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			var symbol = Request.QueryString["symbol"];
			var strike = Request.QueryString["strike"];

			var json = new WebClient().DownloadString("http://www.google.com/finance/option_chain?q="+symbol+"&output=json");

			FinanceContent.Style.Add("display", "none");
			Strike.Style.Add("display", "none");

			FinanceContent.Text = json;
			Strike.Text = strike;

		}
	}
}