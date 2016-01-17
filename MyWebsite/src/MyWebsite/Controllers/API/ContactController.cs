using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;
using MyWebsite.Services;
using Newtonsoft.Json;

namespace MyWebsite.Controllers.API {

	[Produces("application/json")]
	[Route("api/Contact")]
	public class ContactController : Controller {
		private readonly IAbsRepo<Contact> _contacts;
		private readonly IEmailSender _emailSender;

		public ContactController(IAbsRepo<Contact> contacts, IEmailSender emailSender) {
			_contacts = contacts;
			_emailSender = emailSender;
		}

		// POST api/contact
		[HttpPost]
		public bool Post(Contact contact) {
            try {
				Task.Run(() => {
					_emailSender.SendEmailAsync(
						"dbogatov@wpi.edu",
						"You have been contacted!",
						@"Name: " + contact.Name + "\nEmail: " + contact.Email + "\nComment: " + contact.Comment + "\nLanguage: " + contact.Language + "\n\nEnd of message.",
						"Contact Manager"
					);
				});
                contact.RegTime = DateTime.Now;
                contact.Hash = Utility.GetMd5Hash(contact.Email);

                _contacts.AddItem(contact);	
			} catch (System.Exception) {
				return false;
			}

			return true;
		}
	}
}
