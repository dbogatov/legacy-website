using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models.Minesweeper
{

	/// <summary>
	/// Utility class is mainly created for working with DB
	/// </summary>
	public static class Utility
	{
		/// <summary>
		/// Generates a random ID
		/// </summary>
		/// <returns>A string from number - ID</returns>
		public static string generateUserID()
		{
			Random rnd = new Random();
			return rnd.Next(2000000).ToString();
		}

		/// <summary>
		/// Gets a nickname from DB
		/// </summary>
		/// <param name="userID">user's ID</param>
		/// <returns>User's ID, or empty string, if error or ID is not valid.</returns>
		public static string getNickname(int userID)
		{

			var context = new DataContext();

			try
			{
				return context.NickNameIds.FirstOrDefault(nni => nni.UserId == userID).UserNickName;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return "";
			}

		}

		/// <summary>
		/// Adds a pair ID-nickname to DB
		/// </summary>
		/// <param name="userID">User's ID</param>
		/// <param name="nickName">User's nickname</param>
		/// <returns>True, if there is no exception thrown.</returns>
		public static bool addNickNameID(int userID, string nickName)
		{

			var context = new DataContext();

			try
			{
				context.NickNameIds.Add(new NickNameId
				{
					UserId = userID,
					UserNickName = nickName
				});
				return context.SaveChanges() > 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}

		}

		/// <summary>
		/// Updates a pair ID-nickname to DB
		/// </summary>
		/// <param name="userID">User's ID</param>
		/// <param name="nickName">User's nickname</param>
		/// <returns>True, if there is no exception thrown.</returns>
		public static bool updateNickNameID(int userID, string nickName)
		{

			var context = new DataContext();

			try
			{
				context.NickNameIds.FirstOrDefault(nni => nni.UserId == userID).UserNickName = nickName;
				return context.SaveChanges() > 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}

		}

		/// <summary>
		/// Adds a record that this ID has started a game
		/// </summary>
		/// <param name="userID">User's ID</param>
		/// <returns>True, if there is no exception thrown.</returns>
		public static bool addTryID(int userID)
		{

			var context = new DataContext();

			try
			{
				if (context.Gamestats.Any(gs => gs.UserId == userID))
				{
					context.Gamestats.FirstOrDefault(gs => gs.UserId == userID).GamesPlayed++;
					context.Gamestats.FirstOrDefault(gs => gs.UserId == userID).MSStart = DateTime.UtcNow.Ticks;
				}
				else
				{
					//Console.WriteLine("Before " + DateTime.Now);
					context.Gamestats.Add(new Gamestat
					{
						UserId = userID,
						GamesPlayed = 1,
						GamesWon = 0,
						MSStart = DateTime.UtcNow.Ticks
					});
					//Console.WriteLine("After");
				}
				return context.SaveChanges() > 0;
			}
			catch (Exception e)
			{
				Console.WriteLine("Except");
				Console.WriteLine(e.ToString());
				return false;
			}

		}

		/// <summary>
		/// Gets leaders for the given game mode. 
		/// </summary>
		/// <param name="mode">game mode</param>
		/// <returns>A sorted list of leaders.</returns>
		public static IEnumerable<Leader> getLeaderBoard(int mode)
		{

			var context = new DataContext();

			try
			{
				return context.Leaderboards.Include(l => l.NickNameId).Where(l => l.Mode == mode).OrderBy(l => l.Duration).ToList().Select(l => new Leader
				{
					NickName = l.NickNameId.UserNickName,
					Duration = l.Duration
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return new List<Leader>();
			}
		}

		/// <summary>
		/// Adds a record that user has won a game.
		/// </summary>
		/// <param name="userID">User's ID</param>
		/// <param name="mode">game mode</param>
		/// <returns>True, if there is no exception thrown.</returns>
		public static bool addAchievment(int userID, int mode)
		{

			var context = new DataContext();

			try
			{
				var now = DateTime.UtcNow.Ticks;
				var start = context.Gamestats.FirstOrDefault(gs => gs.UserId == userID).MSStart;
				if (context.Leaderboards.Any(l => l.UserId == userID && l.Mode == mode))
				{
					//Console.WriteLine("Leader is here");
					var duration = new TimeSpan(now - start).TotalMilliseconds;
					var leader = context.Leaderboards.FirstOrDefault(l => l.UserId == userID && l.Mode == mode);
					//Console.WriteLine($"{leader.Duration} : {duration}");
					if (leader.Duration > duration)
					{
						//Console.WriteLine("Modify Leader: " + leader.UserId);
						leader.Duration = Convert.ToInt32(duration);
					}
				}
				else
				{
					//Console.WriteLine("Add Leader: " + userID);
					context.Leaderboards.Add(new Leaderboard
					{
						UserId = userID,
						Duration = Convert.ToInt32(new TimeSpan(now - start).TotalMilliseconds),
						Mode = mode
					});
				}
				return context.SaveChanges() > 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}
		}

		public static List<Move> runSolver(string field)
		{
			return SolverLauncher.runSolver(field);
		}
	}
}