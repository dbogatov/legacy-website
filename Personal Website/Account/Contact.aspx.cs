using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using Personal_Website.Models;

namespace Personal_Website.Account {
	public partial class Contact : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			this.Master.FindControl("signInRequest").Visible = false;
		}

		protected void SendInfo_Click(object sender, EventArgs e) {
			var name = UserName.Text;
			var email = Email.Text;
			var story = Comment.Text;
			var lang = Language.SelectedValue;

			var returnUrl = Request.QueryString["returnUrl"] ?? "";

			if (Authentication.Register(email, name, story, lang)) {
				
				new Thread(delegate() {
					EmailManager.sendAfterContact(email, lang, name, story);
				}).Start();

				Response.Redirect(String.Format("~/Notification.aspx?name={0}&message={1}&returnUrl={2}", name, "You have successfully contacted me. I will get in touch shortly.", returnUrl));
			} else {
				Response.Redirect(String.Format("~/Notification.aspx?name={0}&message={1}&returnUrl={2}", name, "You have already registered. Don't try again!", returnUrl));
			}
		}
	}
}