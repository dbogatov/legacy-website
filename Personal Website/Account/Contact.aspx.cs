using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Security.Cryptography;
using System.Text;


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

			var returnUrl = Request.QueryString["returnUrl"] ?? HttpContext.Current.Request.Url.Host;

			if (HttpContext.Current.Request.Cookies["userCookie"] == null) {

				string hash;

				string source = email;
				using (MD5 md5Hash = MD5.Create()) {
					hash = GetMd5Hash(md5Hash, source);

					System.Diagnostics.Debug.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");
				}

				HttpCookie cookieUser = new HttpCookie("userCookie");
				cookieUser.Value = hash;
				cookieUser.Expires = DateTime.MaxValue;
				HttpContext.Current.Response.SetCookie(cookieUser);

				new Thread(delegate() {
					new UsersDataContext().AddUser(name, email, story, lang, hash);
				}).Start();

				Response.Redirect(String.Format("~/Notification.aspx?name={0}&message={1}&returnUrl={2}", name, "You have successfully contacted me. I will get in touch shortly.", returnUrl));
			} else {
				Response.Redirect(String.Format("~/Notification.aspx?name={0}&message={1}&returnUrl={2}", name, "You have already registered. Don't try again!", returnUrl));
			}
		}

		static string GetMd5Hash(MD5 md5Hash, string input) {

			// Convert the input string to a byte array and compute the hash. 
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes 
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data  
			// and format each one as a hexadecimal string. 
			for (int i = 0; i < data.Length; i++) {
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string. 
			return sBuilder.ToString();
		}
	}
}