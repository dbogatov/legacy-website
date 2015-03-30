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

			var symbol = Symbol.Text;
			var strike = Strike.Text;

			if (symbol == "" || strike == "") {
				symbol = Request.QueryString["symbol"];
				strike = Request.QueryString["strike"];
			}

			if (symbol == null || strike == null) {
				symbol = "AAPL";
				strike = "128";
			}


			string json;

			try {
				json = new WebClient().DownloadString("http://www.google.com/finance/option_chain?q=" + symbol + "&output=json");
			} catch (Exception) {
				json = "";				
			}
			

			FinanceContent.Style.Add("display", "none");
			MyStrike.Style.Add("display", "none");
			MySymbol.Style.Add("display", "none");

			FinanceContent.Text = json;
			MyStrike.Text = strike;
			MySymbol.Text = symbol;
		}
	}
}