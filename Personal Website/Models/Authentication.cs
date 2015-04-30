using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Personal_Website.Account;

namespace Personal_Website.Models {
	public static class Authentication {

		/// <summary>
		/// Registers user
		/// </summary>
		/// <param name="email"></param>
		/// <param name="name"></param>
		/// <param name="story"></param>
		/// <param name="lang"></param>
		/// <returns>false if user is already registered, tru otherwise</returns>
		public static bool Register(string email, string name, string story, string lang) {

			if (!isRegistered()) {

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

					UsersDataContext context = new UsersDataContext();
					User u = new User() {
						userName = name,
						userEmail = email,
						userComment = story,
						userLanguage = lang,
						hash = hash,
						regTime = System.DateTime.Now
					};
					context.Users.InsertOnSubmit(u);
					context.SubmitChanges();

					//new UsersDataContext().AddUser(name, email, story, lang, hash);
				}).Start();

				return true;
			} else {
				return false;
			}

		}

		private static string GetMd5Hash(MD5 md5Hash, string input) {

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

		public static void startDemo() {
			HttpCookie cookieUser = new HttpCookie("userCookie");
			cookieUser.Value = "Demo";
			cookieUser.Expires = DateTime.Now.AddDays(3);
			HttpContext.Current.Response.SetCookie(cookieUser);

			HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
		}

		public static bool isRegistered() {
			return HttpContext.Current.Request.Cookies["userCookie"] != null &&	new Personal_Website.Account.UsersDataContext().CheckUser(HttpContext.Current.Request.Cookies["userCookie"].Value) == 1; 
		}

		public static bool isDemo() {
			return HttpContext.Current.Request.Cookies["userCookie"] != null && HttpContext.Current.Request.Cookies["userCookie"].Value == "Demo";
		}

		public static bool grantAccess() {
			return isRegistered() || isDemo();
		}

		public static void clearCookie() {
			HttpCookie cookieUser = new HttpCookie("userCookie");
			cookieUser.Expires = DateTime.Now.AddDays(-1d);
			HttpContext.Current.Response.SetCookie(cookieUser);

			HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
		}

		public static void redirectToHome() {
			HttpContext.Current.Response.Redirect("~/");
		}
	}
}