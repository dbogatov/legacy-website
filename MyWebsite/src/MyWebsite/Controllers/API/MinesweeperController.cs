using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;
using MyWebsite.Services;
using Newtonsoft.Json;
using Microsoft.AspNet.Http;

using MyWebsite.Models.Minesweeper;

namespace MyWebsite.Controllers.API {

	[Produces("application/json")]
	[Route("api/Minesweeper")]
	public class MinesweeperController : Controller {

		public MinesweeperController(IHttpContextAccessor http, IAbsRepo<Leaderboard> leaderboards, IAbsRepo<Gamestat> gamestats, IAbsRepo<NickNameId> nicknames) {
            Field.httpContext = http;
            SolverLauncher.httpContext = http;
            Utility.gameStats = gamestats;
            Utility.leaderboards = leaderboards;
            Utility.nicknames = nicknames;
        }

		[Route("getNickName")]
		[HttpPost]
		public string GetNickName([FromBody] string json) {

			return "Dima4ka";
		}
		
		[Route("openPlace")]
		[HttpPost]
		public string OpenPlace([FromBody] string json) {
			return "openPlace";
		}
		
		[Route("startGame")]
		[HttpPost]
		public bool StartGame([FromBody] string json) {

			return true;
		}
		
		[Route("isWon")]
		[HttpPost]
		public bool IsWon([FromBody] string json) {

			return true;
		}
		
		[Route("isLost")]
		[HttpPost]
		public bool IsLost([FromBody] string json) {

			return true;
		}
		
		[Route("isGameRunning")]
		[HttpPost]
		public bool IsGameRunning([FromBody] string json) {

			return true;
		}
		
		[Route("getAllMines")]
		[HttpPost]
		public string GetAllMines([FromBody] string json) {

			return "Dima4ka";
		}
		
		[Route("getLeaderBoard")]
		[HttpPost]
		public string GetLeaderBoard([FromBody] string json) {

			return "Dima4ka";
		}
		
		[Route("runSolver")]
		[HttpPost]
		public string RunSolver([FromBody] string json) {

			return "Dima4ka";
		}
	}
}
