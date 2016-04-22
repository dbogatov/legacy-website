using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;
using MyWebsite.Services;
using MyWebsite.ViewModels;

namespace MyWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/Email")]
	public class EmailController : Controller
	{
		private readonly IEmailSender _emailSender;

		public EmailController(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		// POST: api/SendEmail
		[HttpPost]
		[Route("SendEmail")]
		public bool SendEmail(EmailViewModel model)
		{
            try
            {
                Task.Run(() =>
                {
                    _emailSender.SendEmailAsync(
                        model.To,
                        model.Subject,
                        model.Message,
                        model.FromName
                    );
                });
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
		}
		
		// POST: api/VisaSupportEmail
		[HttpPost]
		[Route("VisaSupportEmail")]
		public bool VisaSupportEmail(VisaSupportEmailViewModel model) 
		{
			try
            {
                Task.Run(() =>
                {
                    _emailSender.SendEmailAsync(
                        new List<string> { "dbogatov@wpi.edu" },
                        $"Contact added from {new Uri(model.Source).Host.ToLower()}",
                        model.ToString(),
                        "Notificator"
                    );
                });
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
		}
	}
}