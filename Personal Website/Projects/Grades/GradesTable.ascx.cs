using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Personal_Website.Models;


namespace Personal_Website.Projects.Grades {
	public partial class GradesTable : System.Web.UI.UserControl {

		public string Requirement;
		private int maxForUnregistered = (Authentication.isRegistered() || Authentication.isDemo()) ? Int32.MaxValue : 2;

		protected void Page_Load(object sender, EventArgs e) {

			gradeHeader.Visible = Authentication.grantAccess();

			int canary = 0;
			foreach (var g in new GradesDataDataContext().GradesViews) {
				
				if (canary == maxForUnregistered)
					break;

				if (g.reqName.ToString().Equals(Requirement)) {
					canary++;

					HtmlTableRow row = new HtmlTableRow();

					row.Cells.Add(createNormalCell(g.term.ToString()));
					row.Cells.Add(createNormalCell(g.year.ToString()));
					row.Cells.Add(createNormalCell("" + g.title.ToString() + ""));
					if (Authentication.grantAccess()) {
						row.Cells.Add(createNormalCell(g.gradeLetter.ToString()));
					}
					row.Cells.Add(createNormalCell(g.status.ToString()));

					switch (g.status.ToString()) {
						case "Completed":
							row.Attributes.Add("class", "success");
							break;
						case "in progress...":
							row.Attributes.Add("class", "active");
							break;
						case "Registered":
							row.Attributes.Add("class", "warning");
							break;
						default:
							row.Attributes.Add("class", "danger");
							break;
					}

					body.Controls.Add(row);

				}
			}

		}

		private HtmlTableCell createNormalCell(string content) {
			HtmlTableCell cell = new HtmlTableCell();
			cell.Controls.Add(new LiteralControl(content));

			return cell;
		}
	}
}