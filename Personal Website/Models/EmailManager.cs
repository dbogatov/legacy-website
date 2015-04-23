using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Personal_Website.Models {

	public static class EmailManager {

		public static bool sendAfterContact(string email, string language, string name) {
			
			MailMessage mail = new MailMessage();

			mail.From = new MailAddress("author@dbogatov.org", "Dmytro Bogatov"); //IMPORTANT: This must be same as your smtp authentication address.
			mail.To.Add(email);

			string subject = "Lang error";
			string body = "Lang error";

			
			foreach (var dict in new EmailDataContext().Dictionaries) {
				if (dict.userLanguage == language) {
					subject = dict.subject;
					body = String.Format(dict.mailBody, name).Replace("\\n", "\n");
				}
			}

			mail.Subject = subject;
			mail.Body = body + "\n\nDmytro Bogatov\nComputer Science\nClass of 2017\ndbogatov@wpi.edu\n";

			SmtpClient smtp = new SmtpClient("mail.dbogatov.org", 25);

			//IMPORANT:  Your smtp login email MUST be same as your FROM address. 
			smtp.Credentials = new NetworkCredential("author@dbogatov.org", "Otli4no!"); ;
			
			try {
				smtp.Send(mail);
			} catch (Exception)	{
				return false;
			}
			
			return true;
		}
			
	}
}