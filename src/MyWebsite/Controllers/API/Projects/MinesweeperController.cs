using System.Linq;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Http;

using MyWebsite.Models.Minesweeper;
using Microsoft.AspNet.Http.Features;
using System;
using System.Threading;
using MyWebsite.Models;
using MyWebsite.ViewModels.Projects.Minesweeper;

namespace MyWebsite.Controllers.API
{

	[Produces("application/json")]
	[Route("api/Minesweeper")]
	public class MinesweeperController : Controller {

		private readonly ISession session;
		private readonly IReadableStringCollection cookies;
		private readonly IResponseCookies responseCookies;

		private readonly DataContext context;

		public MinesweeperController(IHttpContextAccessor http, DataContext context) {
			Field.session = http.HttpContext.Session;
			SolverLauncher.httpContext = http;
			session = http.HttpContext.Session;
			cookies = http.HttpContext.Request.Cookies;
			responseCookies = http.HttpContext.Response.Cookies;

			this.context = context;
		}

		[Route("testSession")]
		[HttpGet]
		public string TestSession() {

			string testRes = session.GetString("Test");

			if (testRes == null) {
				session.SetString("Test", "999");
				return "was not set";
			} else {
				return "Was set: " + testRes;
			}
		}

		[Route("getNickName")]
		[HttpPost]
		public string GetNickName() {
			return !string.IsNullOrWhiteSpace(cookies["UserID"]) ? Models.Minesweeper.Utility.getNickname(Convert.ToInt32(cookies["UserID"])) : "";
		}

		[Route("openPlace")]
		[HttpPost]
		public string OpenPlace(OpenPlaceWrapper wrapper) {
			var x = wrapper.X;
			var y = wrapper.Y;

			// if this is a first shot, generate field
			if (session.GetObjectFromJson<Place[][]>("Field") == null) {
				session.SetObjectAsJson("Field", Field.initField(x, y, session.GetObjectFromJson<Parameters>("Parameters")));
			}

			// get list of changes, serialize it to JSON string and return it
			var changes = Field.openPlace(x, y);

			var result = JsonConvert.SerializeObject(changes);

			System.Diagnostics.Debug.Write(result + "\n");

			return result;
		}

		[Route("openPlaces")]
		[HttpPost]
		public string OpenPlaces(OpenPlacesWrapper coordinates) {
			var places = coordinates.Places.Select(w => new Coordinate(w.X, w.Y)).ToArray();

			var changes = Field.openPlaces(places);

			var result = JsonConvert.SerializeObject(changes);

			return result;
		}

		[Route("startGame")]
		[HttpPost]
		public bool StartGame(StartGameWrapper wrapper) {

			var width = wrapper.Width;
			var height = wrapper.Height;
			var minesNumber = wrapper.MinesNumber;
			var mode = wrapper.Mode;
			var userName = wrapper.UserName;
			Console.WriteLine("Username is: " + userName);

			session.Clear(); // clear SESSION array

			session.SetObjectAsJson("Parameters", new Parameters(width, height, minesNumber, mode)); // save parameters for the game
			session.SetBoolean("lost", false); // is game lost
			session.SetBoolean("won", false); // is game won
			session.SetInt32("placesDiscovered", 0); // number of discovered places
			session.SetBoolean("solverUsed", false); // true, if solver was used in the game

			var userId = "";

			// if person plays for the first time, generate new ID and cookie for him
			if (string.IsNullOrWhiteSpace(cookies["UserID"])) {
				userId = Models.Minesweeper.Utility.generateUserID();
				responseCookies.Append("UserID", userId);

				Console.WriteLine("Cookie was set");

				// add new record to DB
				new Thread(delegate () {
					Models.Minesweeper.Utility.addNickNameID(Convert.ToInt32(userId), userName);
				}).Start();
			} else if (this.GetNickName() != userName) {  // if user wishes to change his nickname, let him do it
				userId = cookies["UserID"];

				new Thread(delegate () {
					Models.Minesweeper.Utility.updateNickNameID(Convert.ToInt32(userId), userName);
				}).Start();
			}

			// remember ID in the SESSION
			if (string.IsNullOrWhiteSpace(userId)) {
				userId = cookies["UserID"];
			}
			session.SetInt32("UserID", Convert.ToInt32(userId));

			// add a record about the try
			new Thread(delegate () {
				Models.Minesweeper.Utility.addTryID(Convert.ToInt32(userId));
			}).Start();

			return true;
		}

		[Route("isWon")]
		[HttpPost]
		public bool IsWon() {
			var result = session.GetBoolean("won").HasValue ? session.GetBoolean("won").Value : false;
			return result;
		}

		[Route("isLost")]
		[HttpPost]
		public bool IsLost() {
			var result = session.GetBoolean("lost").HasValue ? session.GetBoolean("lost").Value : false;
			return result;
		}

		[Route("isGameRunning")]
		[HttpPost]
		public bool IsGameRunning() {
			byte[] val;
			return session.TryGetValue("Parameters", out val);
		}

		[Route("getAllMines")]
		[HttpPost]
		public string GetAllMines() {

			var changes = Field.getAllMines();

			var result = JsonConvert.SerializeObject(changes);

			return result;
		}

		[Route("getLeaderBoard")]
		[HttpPost]
		public string GetLeaderBoard(GetLeaderBoardWrapper wrapper) {
			var mode = wrapper.Mode;

			var leaders = Models.Minesweeper.Utility.getLeaderBoard(mode);

			var result = JsonConvert.SerializeObject(leaders);

			return result;
		}

		[Route("runSolver")]
		[HttpPost]
		public string RunSolver(RunSolverWrapper wrapper) {

			session.SetBoolean("solverUsed", true);

			var solved = Field.runSolver(wrapper.Json);

			var result = JsonConvert.SerializeObject(solved);

			return result;
		}
	}
}
