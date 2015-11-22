using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Personal_Website.Projects.Pentago {
	public class PentagoController : ApiController {
		[RoutePrefix("api/projects/pentago")]
		public class DownloadController : ApiController {
			[Route("test")]
			[HttpPost]
			public String TestMethod([FromBody] String json) {
				return "Hello, world!";
			}

			[Route("host")]
			[HttpPost]
			public String HostGame([FromBody] String json) {
				return GameModel.HostGame();
			}

			[Route("checkjoin")]
			[HttpPost]
			public bool CheckJoin([FromBody] String json) {
				dynamic data = JsonConvert.DeserializeObject(json);

				return GameModel.CheckJoin((String)data.gameCode);
			}

			[Route("join")]
			[HttpPost]
			public bool Join([FromBody] String json) {
				dynamic data = JsonConvert.DeserializeObject(json);

				return GameModel.JoinGame((String)data.gameCode);
			}

		}



	}
}
