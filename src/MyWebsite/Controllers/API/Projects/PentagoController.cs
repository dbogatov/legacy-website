using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MyWebsite.Models;
using MyWebsite.Models.Projects.Pentago;
using MyWebsite.ViewModels.Projects.Pentago;

namespace MyWebsite.Controllers.API
{

	[Produces("application/json")]
	[Route("api/projects/pentago")]
	public class PentagoController : Controller
	{

		public PentagoController(IHttpContextAccessor http)
		{
			//GameModel.context = context;
			GameModel.cookies = http.HttpContext.Request.Cookies;
			GameModel.responseCookies = http.HttpContext.Response.Cookies;
		}

		[Route("host")]
		[HttpPost]
		public string HostGame()
		{
			return JsonConvert.SerializeObject(GameModel.HostGame());
		}

		[Route("checkjoin")]
		[HttpPost]
		public bool CheckJoin(CheckJoinViewModel model)
		{

			return GameModel.CheckJoin(model.gameCode);
		}

		[Route("join")]
		[HttpPost]
		public bool Join(CheckJoinViewModel model)
		{
			return GameModel.JoinGame(model.gameCode);
		}

		[Route("maketurn")]
		[HttpPost]
		public string MakeTurn(TurnViewModel model)
		{
			return GameModel.MakeTurn(model.x, model.y, model.mark, model.field, model.direction);
		}

		[Route("ismyturn")]
		[HttpPost]
		public bool IsMyTurn()
		{
			return GameModel.IsMyTurn();
		}

		[Route("ismymarkcross")]
		[HttpPost]
		public bool IsMyMarkCross()
		{
			return GameModel.IsMyMarkCross();
		}

		[Route("getfield")]
		[HttpPost]
		public string GetField()
		{
			return GameModel.GetField();
		}

		[Route("getgameresult")]
		[HttpPost]
		public GameResult GetGameResult()
		{
			return GameModel.GetGameResult();
		}

		[Route("getlastturn")]
		[HttpPost]
		public string GetLastTurn()
		{
			return GameModel.GetLastTurn();
		}
	}
}