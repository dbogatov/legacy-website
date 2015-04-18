using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personal_Website.Models {
	public static class Authentication {

		public static bool isRegistered() {
			return HttpContext.Current.Request.Cookies["userCookie"] != null &&	new Personal_Website.Account.UsersDataContext().CheckUser(HttpContext.Current.Request.Cookies["userCookie"].Value) == 1;
		}

	}
}