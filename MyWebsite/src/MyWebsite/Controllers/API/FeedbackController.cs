using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
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

        public FeedbackController(DataContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // POST api/feedback
        [HttpPost]
        public bool Post(Feedback feedback)
        {
            try
            {
                Task.Run(() =>
                {
                    _emailSender.SendEmailAsync(
                        "dbogatov@wpi.edu",
                        feedback.Subject,
                        @"FEEDBACK: " + feedback.Subject + "\n\nURL: " + feedback.Url + "\n\nFrom: " + (feedback.Email != "" ? feedback.Email : "{email not provided}") + ":\n\n" + feedback.Body + "\n\nEnd of feedback.",
                        "Feedback Manager"
                    );
                });

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
