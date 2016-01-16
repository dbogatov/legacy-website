using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;
using Newtonsoft.Json;

namespace MyWebsite.Controllers.API {

    [Produces("application/json")]
    [Route("api/Feedback")]
    public class FeedbackController : Controller
    {
        private readonly IAbsRepo<Feedback> _feedbacks;

        public FeedbackController(IAbsRepo<Feedback> feedbacks)
        {
            _feedbacks = feedbacks;
        }

        // POST api/feedback
        [HttpPost]
		public bool Post(Feedback feedback) {
			try {
				_feedbacks.AddItem(feedback);	
			} catch (System.Exception) {
                return false;
			}

			return true;
        }
    }
}
