using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace Personal_Website.Models {
	public static class DatabaseReset {

		public static void ResetAllStatic() {

			ResetGrades();
			ResetProjects();

		}

		public static void ResetProjects() {

			ExecuteSQLScript("~/Projects/Projects", new Personal_Website.Projects.ProjectsDataContext());
			ExecuteSQLScript("~/Projects/ProjectsProcedures", new Personal_Website.Projects.ProjectsDataContext());

		}

		public static void ResetGrades() {

			ExecuteSQLScript("~/Projects/Grades/GradesMain", new Personal_Website.Projects.Grades.GradesDataDataContext());
			ExecuteSQLScript("~/Projects/Grades/GradesMain_Seed", new Personal_Website.Projects.Grades.GradesDataDataContext());

		}

		private static void ExecuteSQLScript(string scriptName, System.Data.Linq.DataContext dataContext) {

			var dataFile = HostingEnvironment.MapPath(String.Format("{0}.sql", scriptName));
			string script = File.ReadAllText(@dataFile);

			try {
				dataContext.ExecuteCommand(script);
			} catch (Exception) {
				System.Diagnostics.Debug.WriteLine("Something was wrong with the script");
			}
		}
	}
}