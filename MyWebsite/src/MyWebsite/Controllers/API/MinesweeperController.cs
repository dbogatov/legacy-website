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
using Microsoft.AspNet.Http.Features;
using System;
using System.Threading;

namespace MyWebsite.Controllers.API {

	[Produces("application/json")]
	[Route("api/Minesweeper")]
	public class MinesweeperController : Controller {

		private readonly ISession session;
		private readonly IReadableStringCollection cookies;
        private readonly IResponseCookies responseCookies;

        public MinesweeperController(IHttpContextAccessor http, IAbsRepo<Leaderboard> leaderboards, IAbsRepo<Gamestat> gamestats, IAbsRepo<NickNameId> nicknames) {
			Field.httpContext = http;
			SolverLauncher.httpContext = http;
			session = http.HttpContext.Session;
			cookies = http.HttpContext.Request.Cookies;
			responseCookies = http.HttpContext.Response.Cookies;
			Utility.gameStats = gamestats;
			Utility.leaderboards = leaderboards;
			Utility.nicknames = nicknames;
        }

		[Route("getNickName")]
		[HttpPost]
		public string GetNickName([FromBody] string json) {

			return string.IsNullOrWhiteSpace(cookies["UserID"]) ? Utility.getNickname(Convert.ToInt32(cookies["UserID"])) : "";
		}
		
		[Route("openPlace")]
		[HttpPost]
		public string OpenPlace([FromBody] string json) {
			dynamic data = JsonConvert.DeserializeObject(json);
			var x = (int)data.x;
			var y = (int)data.y;

			// if this is a first shot, generate field
			if (session.GetObjectFromJson<Place[][]>("Field") == null) {
				session.SetObjectAsJson("Field", Field.initField(x, y, session.GetObjectFromJson<Parameters>("Parameters")));
			}
			/*
			if (HttpContext.Current.Session["Field"] == null ) {
				HttpContext.Current.Session["Field"] = Field.initField(x, y, (Parameters)HttpContext.Current.Session["Parameters"]);			
			}*/

			// get list of changes, serialize it to JSON string and return it
			var changes = Field.openPlace(x, y);

			var result = JsonConvert.SerializeObject(changes);

			System.Diagnostics.Debug.Write(result+"\n");

			return result;
		}
		
		[Route("openPlaces")]
		[HttpPost]
		public string OpenPlaces([FromBody] string json) {
			var places = (Coordinate[])JsonConvert.DeserializeObject(json);

            var changes = Field.openPlaces(places);

			var result = JsonConvert.SerializeObject(changes);

			return result;
		}
		
		[Route("startGame")]
		[HttpPost]
		public bool StartGame([FromBody] string json) {
            Console.WriteLine("Here it is: " + json);
            dynamic data = JsonConvert.DeserializeObject(json);
            var width = (int)data.width;
			var height = (int)data.height;
			var minesNumber = (int)data.minesNumber;
			var mode = (int)data.mode;
            var userName = (string)data.mode;
			Console.WriteLine("Username is: " + userName);

            session.Clear(); // clear SESSION array

			session.SetObjectAsJson("Parameters", new Parameters(width, height, minesNumber, mode)); // save parameters for the game
			session.SetBoolean("lost", false); // is game lost
			session.SetBoolean("won", false); // is game won
            session.SetInt32("placesDiscovered", 0); // number of discovered places
			session.SetBoolean("solverUsed", false); // true, if solver was used in the game

			// if person plays for the first time, generate new ID and cookie for him
            if (string.IsNullOrWhiteSpace(cookies["UserID"])) {
                var userId = Utility.generateUserID();
                responseCookies.Append("UserID", userId);

				// add new record to DB
                new Thread(delegate() {
                    Utility.addNickNameID(Convert.ToInt32(userId), userName);
                }).Start();
            } else if (this.GetNickName("") != userName) {	// if user wishes to change his nickname, let him do it
                int userIDtemp = Convert.ToInt32(cookies["UserID"]);

                new Thread(delegate() {
                    Utility.updateNickNameID(userIDtemp, userName);
                }).Start();
            }

			// remember ID in the SESSION
            int userID = Convert.ToInt32(cookies["UserID"]);
            session.SetInt32("UserID", userID);

			// add a record about the try
            new Thread(delegate() {
                Utility.addTryID(userID);
            }).Start();
            
            return true;
		}
		
		[Route("isWon")]
		[HttpPost]
		public bool IsWon([FromBody] string json) {
			return session.GetBoolean("won").HasValue ? session.GetBoolean("won").Value : false;
		}
		
		[Route("isLost")]
		[HttpPost]
		public bool IsLost([FromBody] string json) {
			return session.GetBoolean("lost").HasValue ? session.GetBoolean("lost").Value : false;
		}
		
		[Route("isGameRunning")]
		[HttpPost]
		public bool IsGameRunning([FromBody] string json) {
            byte[] val;
            return session.TryGetValue("Parameters", out val);
		}
		
		[Route("getAllMines")]
		[HttpPost]
		public string GetAllMines([FromBody] string json) {

			var changes = Field.getAllMines();

			var result = JsonConvert.SerializeObject(changes);

			return result;
		}
		
		[Route("getLeaderBoard")]
		[HttpPost]
		public string GetLeaderBoard([FromBody] string json) {
			dynamic data = JsonConvert.DeserializeObject(json);
            var mode = (int)data.mode;

            var leaders = Utility.getLeaderBoard(mode);

            var result = JsonConvert.SerializeObject(leaders);

			return result;
		}
		
		[Route("runSolver")]
		[HttpPost]
		public string RunSolver([FromBody] string json) {

			session.SetBoolean("solverUsed",  true);

			var solved = Field.runSolver(json);

			var result = JsonConvert.SerializeObject(solved);

			return result;
		}
	}
}
