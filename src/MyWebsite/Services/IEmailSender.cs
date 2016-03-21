using MailKit.Net.Smtp;
using MimeKit;


namespace MyWebsite.Services
{
	public interface IEmailSender
	{
		void SendEmailAsync(string to, string subject, string message, string displayFrom, string from = "author@dbogatov.org");
	}

	public class DefaultEmailSender : IEmailSender
	{
		public void SendEmailAsync(string to, string subject, string text, string displayFrom, string from = "author@dbogatov.org")
		{

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(displayFrom, from));
			message.To.Add(new MailboxAddress("", to));
			message.Subject = subject;

			message.Body = new TextPart("plain")
			{
				Text = text
			};

			using (var client = new SmtpClient())
			{
				client.Connect("mail.dbogatov.org", 25, false);

				// Note: since we don't have an OAuth2 token, disable
				// the XOAUTH2 authentication mechanism.
				client.AuthenticationMechanisms.Remove("XOAUTH2");

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate(from, "Otli4no!");

				client.Send(message);
				client.Disconnect(true);
			}
		}
	}
}
