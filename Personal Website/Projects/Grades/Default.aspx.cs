using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.IO;
using System.Web.UI.HtmlControls;

namespace Personal_Website.Projects.Grades {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			//CreateAndSeedDB();

			HtmlTable table = new HtmlTable();

			table.Attributes.Add("class", "table table-striped table-bordered table-hover");			

			HtmlTableRow header = new HtmlTableRow();

			header.Cells.Add(createHeaderCell("Term"));
			header.Cells.Add(createHeaderCell("Year"));
			header.Cells.Add(createHeaderCell("Title"));
			header.Cells.Add(createHeaderCell("Course ID"));
			header.Cells.Add(createHeaderCell("% Grade"));
			header.Cells.Add(createHeaderCell("Grade"));
			header.Cells.Add(createHeaderCell("Status"));

			table.Rows.Add(header);

			foreach (var g in new GradesDataDataContext().SimpleGrades) {
				HtmlTableRow row = new HtmlTableRow();

				row.Cells.Add(createNormalCell(g.term.ToString()));
				row.Cells.Add(createNormalCell(g.year.ToString()));
				row.Cells.Add(createNormalCell(g.title.ToString()));
				row.Cells.Add(createNormalCell(g.courseID.ToString()));
				row.Cells.Add(createNormalCell(g.gradePercent.ToString()));
				row.Cells.Add(createNormalCell(g.gradeLetter.ToString()));
				row.Cells.Add(createNormalCell(g.status.ToString()));

				table.Rows.Add(row);
			}

			gradesTable.Controls.Clear();
			gradesTable.Controls.Add(table);

		}

		private void CreateAndSeedDB() {

			ExecuteSQLScript("GradesMain");
			ExecuteSQLScript("GradesMain_Seed");
			
		}

		private void ExecuteSQLScript(string scriptName) {

			var dataFile = Server.MapPath(String.Format("~/Projects/Grades/{0}.sql", scriptName));
			string script = File.ReadAllText(@dataFile);

			try {
				new GradesDataDataContext().ExecuteCommand(script);
			} catch (Exception) {
				System.Diagnostics.Debug.WriteLine("Something was wrong with the script");
			}
		}

		private HtmlTableCell createHeaderCell(string content) {
			HtmlTableCell cell = new HtmlTableCell("th");
			cell.Controls.Add(new LiteralControl(content));

			return cell;
		}

		private HtmlTableCell createNormalCell(string content) {
			HtmlTableCell cell = new HtmlTableCell();
			cell.Controls.Add(new LiteralControl(content));

			return cell;
		}
	}
}