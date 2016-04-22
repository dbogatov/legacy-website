using System.Collections.Generic;

namespace MyWebsite.ViewModels
{
	public class EmailViewModel
	{
		public string Message { get; set; }
		public IEnumerable<string> To { get; set; }
		public string FromEmail { get; set; }
		public string FromName { get; set; }
		public string Subject { get; set; }
    }
	
	public class VisaSupportEmailViewModel
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Source { get; set; }

        public override string ToString()
        {
            return $@"
				Check GetResponse!
				
				Someone has just been added to GetResponse.
				
				Name: {this.Name.Placeholder("name no set")}
				Phone: {this.Phone.Placeholder("phone no set")}
				Email: {this.Email.Placeholder("email no set")}
				Source: {this.Source.Placeholder("source no set")}
				
				Always yours,
				Notificator
			";

        }
    }
}