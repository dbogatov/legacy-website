using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;
using MyWebsite.Services;
using Newtonsoft.Json;

namespace MyWebsite.Controllers.API {

	[Produces("application/json")]
	[Route("api/Feedback")]
	public class FeedbackController : Controller {
		private readonly IAbsRepo<Feedback> _feedbacks;
		private readonly IEmailSender _emailSender;

		public FeedbackController(IAbsRepo<Feedback> feedbacks, IEmailSender emailSender) {
			_feedbacks = feedbacks;
			_emailSender = emailSender;
		}

		// POST api/feedback
		[HttpPost]
		public bool Post(Feedback feedback) {
			try {
				Task.Run(() => {
					_emailSender.SendEmailAsync(
						"dbogatov@wpi.edu",
						feedback.Subject,
						@"FEEDBACK: " + feedback.Subject + "\n\nURL: " + feedback.Url + "\n\nFrom: " + (feedback.Email != "" ? feedback.Email : "{email not provided}") + ":\n\n" + feedback.Body + "\n\nEnd of feedback.",
						"Feedback Manager"
					);
				});

				_feedbacks.AddItem(feedback);	
			} catch (System.Exception) {
				return false;
			}

			return true;
		}
	}
}
