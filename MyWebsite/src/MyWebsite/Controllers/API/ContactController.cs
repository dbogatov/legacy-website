using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;
using MyWebsite.Services;

namespace MyWebsite.Controllers.API
{

    [Produces("application/json")]
	[Route("api/Contact")]
	public class ContactController : Controller {
		private readonly IEmailSender _emailSender;
        private readonly DataContext _context;
        private readonly ICryptoService _crypto;

        public ContactController(DataContext context, IEmailSender emailSender, ICryptoService crypto) {
            _context = context;
            _emailSender = emailSender;
            _crypto = crypto;
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
                contact.Hash = _crypto.CalculateHash(contact.Email);

                _context.Contacts.Add(contact);
                _context.SaveChanges();
            } catch (System.Exception) {
				return false;
			}

			return true;
		}
	}
}
