using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Personal_Website.Account;

namespace Personal_Website.Admin {
	public partial class Users : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			foreach (var u in new UsersDataContext().Users) {

				HtmlTableRow row = new HtmlTableRow();

				row.Cells.Add(createNormalCell(u.userName.ToString()));
				row.Cells.Add(createNormalCell(u.userEmail.ToString()));
				row.Cells.Add(createNormalCell(u.userComment.ToString() == "" ? "Not provided" : u.userComment.ToString()));
				row.Cells.Add(createNormalCell(u.userLanguage.ToString()));
				row.Cells.Add(createNormalCell(u.regTime.ToShortDateString()));

				body.Controls.Add(row);
			}
		}

		private HtmlTableCell createNormalCell(string content) {
			HtmlTableCell cell = new HtmlTableCell();
			cell.Controls.Add(new LiteralControl(content));

			return cell;
		}
	}
}