using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

		// POST: api/Email/SendEmail
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
		[EnableCors("AllowSpecificOrigin")] 
		public bool VisaSupportEmail(VisaSupportEmailViewModel model) 
		{
			try
            {
                Task.Run(() =>
                {
                    _emailSender.SendEmailAsync(
                        new List<string> { 
							"dima4ka007@gmail.com",
							"smirnova.jn22@gmail.com",
							"milya-i@mail.ru"
						},
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