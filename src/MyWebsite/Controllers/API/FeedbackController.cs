using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;
using MyWebsite.Services;

namespace MyWebsite.Controllers.API
{

	[Produces("application/json")]
	[Route("api/Feedback")]
	public class FeedbackController : Controller
	{
		private readonly DataContext _context;
		private readonly IEmailSender _emailSender;
		private readonly IPushService _pushService;

		public FeedbackController(DataContext context, IEmailSender emailSender, IPushService pushService)
		{
			_context = context;
			_emailSender = emailSender;
            _pushService = pushService;
        }

		// POST api/feedback
		[HttpPost]
		public bool Post(Feedback feedback)
		{
            _pushService.SendToTelegram(@"FEEDBACK: " + feedback.Subject + "\n\nURL: " + feedback.Url + "\n\nFrom: " + (feedback.Email != "" ? feedback.Email : "{email not provided}") + ":\n\n" + feedback.Body + "\n\nEnd of feedback.");

            try
			{
				// Task.Run(() =>
				// {
				// 	_emailSender.SendEmailAsync(
				// 		new List<string> { "dbogatov@wpi.edu" },
				// 		feedback.Subject,
				// 		@"FEEDBACK: " + feedback.Subject + "\n\nURL: " + feedback.Url + "\n\nFrom: " + (feedback.Email != "" ? feedback.Email : "{email not provided}") + ":\n\n" + feedback.Body + "\n\nEnd of feedback.",
				// 		"Feedback Manager"
				// 	);
				// });

				_context.Add(feedback);
				_context.SaveChanges();
			}
			catch (System.Exception)
			{
				return false;
			}

			return true;
		}
	}
}
