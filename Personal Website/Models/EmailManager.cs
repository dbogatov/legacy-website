using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Personal_Website.Models {

	public static class EmailManager {

		private static bool sendEmail(string to, string from, string display, string subject, string body) {
			MailMessage mail = new MailMessage();

			mail.From = new MailAddress(from, display); //IMPORTANT: This must be same as your smtp authentication address.
			mail.To.Add(to);

			mail.Subject = subject;
			mail.Body = body;

			SmtpClient smtp = new SmtpClient("mail.dbogatov.org", 25);

			//IMPORANT:  Your smtp login email MUST be same as your FROM address. 
			smtp.Credentials = new NetworkCredential(from, "Otli4no!");

			try {
				smtp.Send(mail);
			} catch (Exception) {
				return false;
			}

			return true;
		}

		public static bool sendFeedback(string subject, string body, string from, string url) {
			return sendEmail("dbogatov@wpi.edu", "author@dbogatov.org", "Feedback Manager", "FEEDBACK: " + subject, "URL: "+url+"\n\nFrom " + (from != "" ? from : "{email not provided}") + ":\n\n" + body + "\n\nEnd of feedback.");
		}

		public static bool sendAfterContact(string email, string language, string name, string story) {
			
			string subject = "Lang error";
			string body = "Lang error";
			string aBody = "My lord,\n\nYou have been contacted by " + name + " (" + email + ").\n\nHis/her story:\n" + story + "\n\nAlways yours,\nNotificator";
			
			foreach (var dict in new EmailDataContext().Dictionaries) {
				if (dict.userLanguage == language) {
					subject = dict.subject;
					body = String.Format(dict.mailBody, name).Replace("\\n", "\n");
				}
			}

			body += "\n\nDmytro Bogatov\nComputer Science\nClass of 2017\ndbogatov@wpi.edu\n";

			

			return (sendEmail(email, "author@dbogatov.org", "Dmytro Bogatov", subject, body) &&
				sendEmail("dbogatov@wpi.edu", "author@dbogatov.org", "Notificator", "You have been contacted!", aBody));
		}
			
	}
}