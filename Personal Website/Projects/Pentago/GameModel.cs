using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Personal_Website.Projects.Pentago {
	public static class GameModel {

		private static String GenerateCode(int length) {
			Thread.Sleep(10);
			const String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
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
			var game = context.PentagoGames.First(g => g.GameCode.Equals(gameCode));

			if (game != null) {

				game.JoinKey = joinKey;
				context.SubmitChanges();

				var gameCookie = new HttpCookie("PentagoCookie") {
					["Role"] = "Join",
					["Key"] = joinKey,
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
					GameCode = code,
					HostIsCross = hostIsCross,
					IsHostTurn = hostIsCross,
					IsGameEnded = false,
					GameField = "TBD"
				});
				context.SubmitChanges();
			} catch (Exception) {
				return "error";
			}

			var gameCookie = new HttpCookie("PentagoCookie") {
				["Role"] = "Host",
				["Key"] = hostKey,
				Expires = DateTime.Now.AddDays(1d)
			};
			HttpContext.Current.Response.Cookies.Add(gameCookie);

			return code;
		}

	}
}