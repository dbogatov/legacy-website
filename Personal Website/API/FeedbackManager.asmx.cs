using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Threading;
using Personal_Website.Models;

namespace Personal_Website.API {
	/// <summary>
	/// Summary description for FeedbackManager
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class FeedbackManager : System.Web.Services.WebService {

		[WebMethod]
		public bool leaveFeedback(string from, string subject, string body, string url) {

			new Thread(delegate() {
				EmailManager.sendFeedback(subject, body, from, url);
			}).Start();

			return true;
		}
	}
}
