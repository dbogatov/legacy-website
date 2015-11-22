using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Personal_Website.Projects.Pentago {
	public static class GameModel {

		private static String GenerateCode(int length) {
			Thread.Sleep(10);
			const String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
			var random = new Random();
			return new String(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static bool CheckJoin(String gameCode) {
			return new PentagoDataContext().PentagoGames.Any(g => g.GameCode.Equals(gameCode) && g.JoinKey != null);
		}

		public static bool JoinGame(String gameCode) {
			var joinKey = GenerateCode(32);

			var context = new PentagoDataContext();
			var game = context.PentagoGames.First(g => g.GameCode.Equals(gameCode.ToUpper()));

			if (game != null) {

				game.JoinKey = joinKey;
				context.SubmitChanges();

				var gameCookie = new HttpCookie("PentagoCookie") {
					["Role"] = "Join",
					["Key"] = joinKey,
					["Code"] = game.GameCode,
					Expires = DateTime.Now.AddDays(1d)
				};

				HttpContext.Current.Response.Cookies.Add(gameCookie);
			} else {
				return false;
			}

			return true;
		}

		public static String HostGame() {

			var hostKey = GenerateCode(32);
			var code = GenerateCode(6);
			var hostIsCross = new Random().NextDouble() > 0.5;

			var context = new PentagoDataContext();

			try {
				context.PentagoGames.InsertOnSubmit(new PentagoGame {
					HostKey = hostKey,
					GameCode = code.ToUpper(),
					HostIsCross = hostIsCross,
					IsHostTurn = hostIsCross,
					IsGameEnded = false,
					GameField = new PentagoField().GetField()
				});
				context.SubmitChanges();
			} catch (Exception) {
				return "error";
			}

			var gameCookie = new HttpCookie("PentagoCookie") {
				["Role"] = "Host",
				["Key"] = hostKey,
				["Code"] = code,
				Expires = DateTime.Now.AddDays(1d)
			};

			HttpContext.Current.Response.Cookies.Add(gameCookie);

			return code;
		}

		public static String MakeTurn(int x, int y, Cell mark, Quadrant field, RotDirection direction) {
			if (IsValidTurn(x, y, mark)) {

				var context = new PentagoDataContext();
				var game = context.PentagoGames.First(g => g.GameCode.Equals(HttpContext.Current.Request.Cookies["PentagoCookie"]["Code"]));

				var fieldObj = new PentagoField(game.GameField);
				if (fieldObj.MakeTurn(x, y, mark, field, direction)) {
					var newFieldObj = fieldObj.GetField();

					try {
						game.GameField = newFieldObj;
						context.SubmitChanges();
					} catch (Exception) {
						return "error";
					}

					return newFieldObj;
				}

				return "error";
			} else {
				return "error";
			}
		}

		private static bool IsValidTurn(int x, int y, Cell mark) {
			if (x > 5 || x < 0 || y > 5 || y < 0) {
				return false;
			}

			var game = new PentagoDataContext().PentagoGames.First(g => g.GameCode.Equals(HttpContext.Current.Request.Cookies["PentagoCookie"]["Code"]));
			var role = HttpContext.Current.Request.Cookies["PentagoCookie"]["Role"];

			if (role.Equals("Host") && game.IsHostTurn && (game.HostIsCross && mark.Equals(Cell.Cross) || !game.HostIsCross && mark.Equals(Cell.Donut))) {
				return true;
			}

			if (role.Equals("Join") && !game.IsHostTurn && (!game.HostIsCross && mark.Equals(Cell.Cross) || game.HostIsCross && mark.Equals(Cell.Donut))) {
				return true;
			}

			return false;
		}

		public static bool IsMyTurn() {
			var game = new PentagoDataContext().PentagoGames.First(g => g.GameCode.Equals(HttpContext.Current.Request.Cookies["PentagoCookie"]["Code"]));
			var role = HttpContext.Current.Request.Cookies["PentagoCookie"]["Role"];

			return role.Equals("Host") && game.IsHostTurn || role.Equals("Join") && !game.IsHostTurn;
		}

		public static String GetField() {
			return
				new PentagoDataContext().PentagoGames.First(
					g => g.GameCode.Equals(HttpContext.Current.Request.Cookies["PentagoCookie"]["Code"])).GameField;
		}

		public static bool IsMyMarkCross() {
			var game = new PentagoDataContext().PentagoGames.First(g => g.GameCode.Equals(HttpContext.Current.Request.Cookies["PentagoCookie"]["Code"]));
			var role = HttpContext.Current.Request.Cookies["PentagoCookie"]["Role"];

			return role.Equals("Host") && game.HostIsCross || role.Equals("Join") && !game.HostIsCross;
		}

	}
}