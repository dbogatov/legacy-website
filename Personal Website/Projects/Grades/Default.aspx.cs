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

			var courseList = new List<object>();

			foreach (var g in new GradesDataDataContext().SimpleGrades) {
				courseList.Add(new object[] { g.term, g.year, g.title, g.courseID, g.gradePercent, g.gradeLetter, g.status, "Functionality to be developed", "Functionality to be developed" });
			}

			this.Store.DataSource = courseList.ToArray<object>();
			this.Store.DataBind();			
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