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

			[Route("maketurn")]
			[HttpPost]
			public String MakeTurn([FromBody] String json) {
				dynamic data = JsonConvert.DeserializeObject(json);

				var x = (int)data.x;
				var y = (int)data.y;
				var mark = (Cell)data.mark;
				var field = (Quadrant)data.field;
				var direction = (RotDirection)data.direction;

				return GameModel.MakeTurn(x, y, mark, field, direction);
			}

			[Route("ismyturn")]
			[HttpPost]
			public bool IsMyTurn([FromBody] String json) {
				return GameModel.IsMyTurn();
			}

			[Route("ismymarkcross")]
			[HttpPost]
			public bool IsMyMarkCross([FromBody] String json) {
				return GameModel.IsMyMarkCross();
			}

			[Route("getfield")]
			[HttpPost]
			public String GetField([FromBody] String json) {
				return GameModel.GetField();
			}

			[Route("getgameresult")]
			[HttpPost]
			public GameResult GetGameResult([FromBody] String json) {
				return GameModel.GetGameResult();
			}

		}



	}
}
