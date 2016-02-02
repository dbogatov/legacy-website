using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;

namespace MyWebsite.Models.Minesweeper {

	/// <summary>
	/// Utility class is mainly created for working with DB
	/// </summary>
	public static class Utility {

		public static AbsDbContext context { get; set; }

        //public static IServiceProvider services;

        /// <summary>
        /// Generates a random ID
        /// </summary>
        /// <returns>A string from number - ID</returns>
        public static string generateUserID() {
			Random rnd = new Random();
			return rnd.Next(2000000).ToString();
		}

		/// <summary>
		/// Gets a nickname from DB
		/// </summary>
		/// <param name="userID">user's ID</param>
		/// <returns>User's ID, or empty string, if error or ID is not valid.</returns>
		public static string getNickname(int userID) {

			//nicknames = (IAbsRepo<NickNameId>)services.GetService(typeof(IAbsRepo<NickNameId>));

			try {
                Console.WriteLine("Trying to get nicknmae for " + userID);
                return context.NickNameIds.FirstOrDefault(nni => nni.UserId == userID).UserNickName; //nicknames.GetItem(userID).UserNickName;
                                                             //return new DataDataContext().getNickname(userID).ToList<getNicknameResult>().First().UserNickName;	
            } catch (Exception e) {
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
		public static bool addNickNameID(int userID, string nickName) {

			try {
                context.NickNameIds.Add(new NickNameId {
					UserId = userID,
					UserNickName = nickName
				});
                return context.SaveChanges() > 0;
			} catch (Exception e) {
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
		public static bool updateNickNameID(int userID, string nickName) {

			//nicknames = (IAbsRepo<NickNameId>)services.GetService(typeof(IAbsRepo<NickNameId>));

			try {
                Console.WriteLine("UPD NN");
                context.NickNameIds.FirstOrDefault(nni => nni.UserId == userID).UserNickName = nickName;
                return context.SaveChanges() > 0;
            } catch (Exception e) {
				Console.WriteLine(e.ToString());
				return false;
			}

		}

		/// <summary>
		/// Adds a record that this ID has started a game
		/// </summary>
		/// <param name="userID">User's ID</param>
		/// <returns>True, if there is no exception thrown.</returns>
		public static bool addTryID(int userID) {

			//gameStats = (IAbsRepo<Gamestat>)services.GetService(typeof(IAbsRepo<Gamestat>));

			try {
				if (context.Gamestats.Any(gs => gs.UserId == userID))
				{
                    context.Gamestats.FirstOrDefault(gs => gs.UserId == userID).GamesPlayed++;
                }
				else
				{
                    context.Gamestats.Add(new Gamestat {
						UserId = userID,
						GamesPlayed = 1,
						GamesWon = 0,
						DateStart = DateTime.Now
					});
                }
                return context.SaveChanges() > 0;
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
				return false;
			}

		}

		/// <summary>
		/// Gets leaders for the given game mode. 
		/// </summary>
		/// <param name="mode">game mode</param>
		/// <returns>A sorted list of leaders.</returns>
		public static IEnumerable<Leader> getLeaderBoard(int mode) {

			//leaderboards = (IAbsRepo<Leaderboard>)services.GetService(typeof(IAbsRepo<Leaderboard>));
			//nicknames = (IAbsRepo<NickNameId>)services.GetService(typeof(IAbsRepo<NickNameId>));

			try {
                Console.WriteLine("GET LB for mode " + mode);
                return context.Leaderboards.Where(l => l.Mode == mode).OrderBy(l => l.Duration).ToList().Select(l => new Leader
                {
                    NickName = l.NickNameId.UserNickName,
                    Duration = l.Duration
            });
				//return new DataDataContext().getLeaderBoard(mode).ToList<getLeaderBoardResult>();
			} catch (Exception e) {
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
		public static bool addAchievment(int userID, int mode) {

			//leaderboards = (IAbsRepo<Leaderboard>)services.GetService(typeof(IAbsRepo<Leaderboard>));
			//gameStats = (IAbsRepo<Gamestat>)services.GetService(typeof(IAbsRepo<Gamestat>));

			try {
				
				
				
				var now = DateTime.Now;
                var start = context.Gamestats.FirstOrDefault(gs => gs.UserId == userID).DateStart.Value; //gameStats.GetItem(userID).DateStart;
				if (context.Leaderboards.Any(l => l.UserId == userID && l.Mode == mode))
				{
					var duration = (now - start).TotalMilliseconds;
                    var leader = context.Leaderboards.FirstOrDefault(l => l.UserId == userID && l.Mode == mode);
					if (leader.Duration > duration) {
						leader.Duration = Convert.ToInt32(duration);
                        leader.DateStart = start;
                        leader.DateEnd = now;
                    }
                }
				else
				{
                    context.Leaderboards.Add(new Leaderboard {
						UserId = userID,
						DateStart = start,
						DateEnd = now,
						Duration = Convert.ToInt32((now - start).TotalMilliseconds),
						Mode = mode
					});
                }
                return context.SaveChanges() > 0;
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
				return false;
			}
		}

		public static List<Move> runSolver(string field) {
			return SolverLauncher.runSolver(field);
		}
	}
}