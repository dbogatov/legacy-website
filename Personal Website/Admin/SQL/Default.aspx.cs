using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Personal_Website.Admin.SQL {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			if (Page.IsPostBack) {
				var connectionString = ConnString.Text;
				var query = SQLQuery.Text;
				SqlCommand command;
				SqlConnection cnn;

				var result = 0;

				try {
					cnn = new SqlConnection(connectionString);
				} catch (Exception ex) {

					SQLResponse.Text = "Something is wrong with your connection string";
					Message.Text = ex.Message;
					return;
				}
				
            
				try
				{
					cnn.Open();

					command = new SqlCommand(query, cnn);

					result = command.ExecuteNonQuery();

					cnn.Close();
				}
				catch (Exception ex)
				{
					SQLResponse.Text = "Something is wrong with your query";
					Message.Text = ex.Message;
					return;
				}

				SQLResponse.Text = "Number of rows affected = " + result;
				Message.Text = "Looks like everything is fine...";
			}
		}
	}
}