using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using MyWebsite.Models.Enitites;
using Newtonsoft.Json;

namespace MyWebsite.Models.Projects.Pentago
{
	public enum GameResult
	{
		Win, Lose, Tie, Progress
	}

	public static class GameModel
	{
		public static IRequestCookieCollection cookies;
		public static IResponseCookies responseCookies;

		private static String GenerateCode(int length)
		{
			Thread.Sleep(10);
			const String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
			var random = new Random();
			return new String(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static bool CheckJoin(String gameCode)
		{
			var context = new DataContext();

			return context.PentagoGames.Any(g => g.GameCode.Equals(gameCode) && g.JoinKey != null);
		}

		public static bool JoinGame(String gameCode)
		{
			var joinKey = GenerateCode(32);

			var context = new DataContext();
			var game = context.PentagoGames.First(g => g.GameCode.Equals(gameCode.ToUpper()));

			if (game != null)
			{

				game.JoinKey = joinKey;
				context.SaveChanges();

				responseCookies.Append("PentagoCookieRole", "Join");
				responseCookies.Append("PentagoCookieKey", joinKey);
				responseCookies.Append("PentagoCookieCode", game.GameCode);
			}
			else
			{
				return false;
			}

			return true;
		}

		public static String HostGame()
		{

			var hostKey = GenerateCode(32);
			var code = GenerateCode(6);
			var hostIsCross = new Random().NextDouble() > 0.5;

			try
			{
				var context = new DataContext();
				context.PentagoGames.Add(new PentagoGame
				{
					HostKey = hostKey,
					GameCode = code.ToUpper(),
					HostIsCross = hostIsCross,
					IsHostTurn = hostIsCross,
					IsGameEnded = false,
					GameField = new PentagoField().GetField()
				});
				context.SaveChanges();
			}
			catch (Exception)
			{
				return "error";
			}

			responseCookies.Append("PentagoCookieRole", "Host");
			responseCookies.Append("PentagoCookieKey", hostKey);
			responseCookies.Append("PentagoCookieCode", code);

			return code;
		}

		public static GameResult GetGameResult()
		{
			var context = new DataContext();
			var game = context.PentagoGames.First(g => g.GameCode.Equals(cookies["PentagoCookieCode"]));
			return new PentagoField(game.GameField).GetGameResult(GetMyMark());
		}

		public static String MakeTurn(int x, int y, Cell mark, Quadrant field, RotDirection direction)
		{
			if (!IsValidTurn(x, y, mark)) return "error";

			var context = new DataContext();
			var game = context.PentagoGames.First(g => g.GameCode.Equals(cookies["PentagoCookieCode"]));

			var fieldObj = new PentagoField(game.GameField);

			if (!fieldObj.MakeTurn(x, y, mark, field, direction)) return "error";

			var newFieldObj = fieldObj.GetField();

			try
			{
				game.GameField = newFieldObj;
				game.IsHostTurn = !game.IsHostTurn;
				game.LastTurn = JsonConvert.SerializeObject(new
				{
					x,
					y,
					mark,
					field,
					direction
				});
				context.SaveChanges();
			}
			catch (Exception)
			{
				return "error";
			}

			return newFieldObj;
		}

		public static String GetLastTurn()
		{
			var context = new DataContext();
			return
				context.PentagoGames.First(
					g => g.GameCode.Equals(cookies["PentagoCookieCode"])).LastTurn;
		}

		private static bool IsValidTurn(int x, int y, Cell mark)
		{
			if (x > 5 || x < 0 || y > 5 || y < 0)
			{
				return false;
			}

			string role = cookies["PentagoCookieRole"];

			var context = new DataContext();
			var game = context.PentagoGames.First(g => g.GameCode.Equals(cookies["PentagoCookieCode"]));

			if (role.Equals("Host") && game.IsHostTurn && (game.HostIsCross && mark.Equals(Cell.Cross) || !game.HostIsCross && mark.Equals(Cell.Donut)))
			{
				return true;
			}

			if (role.Equals("Join") && !game.IsHostTurn && (!game.HostIsCross && mark.Equals(Cell.Cross) || game.HostIsCross && mark.Equals(Cell.Donut)))
			{
				return true;
			}

			return false;
		}

		public static bool IsMyTurn()
		{
			string role = cookies["PentagoCookieRole"];

			var context = new DataContext();
			var game = context.PentagoGames.First(g => g.GameCode.Equals(cookies["PentagoCookieCode"]));

			return role.Equals("Host") && game.IsHostTurn || role.Equals("Join") && !game.IsHostTurn;
		}

		public static String GetField()
		{
			var context = new DataContext();
			return
				context.PentagoGames.First(
					g => g.GameCode.Equals(cookies["PentagoCookieCode"])).GameField;
		}

		public static bool IsMyMarkCross(PentagoGame game = null)
		{
			string role = cookies["PentagoCookieRole"];

			var context = new DataContext();

			game = game ?? context.PentagoGames.First(g => g.GameCode.Equals(cookies["PentagoCookieCode"]));

			return role.Equals("Host") && game.HostIsCross || role.Equals("Join") && !game.HostIsCross;
		}

		public static Cell GetMyMark(PentagoGame game = null)
		{
			return IsMyMarkCross(game) ? Cell.Cross : Cell.Donut;
		}

	}
}