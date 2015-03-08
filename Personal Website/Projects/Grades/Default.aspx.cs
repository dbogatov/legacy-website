using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.IO;

namespace Personal_Website.Projects.Grades {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

		}

		protected string GetGrades() {
			CreateAndSeedDB();

			StringBuilder html = new StringBuilder();

			var gradesData = new GradesDataDataContext().SimpleGrades;

			foreach (var g in gradesData) {
				html.Append(String.Format(
					"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
					g.term, g.year, g.title, g.courseID, g.gradePercent, g.gradeLetter, g.status
					));
			}

			return html.ToString();
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
	}
}