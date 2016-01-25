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
		
		public static IAbsRepo<Leaderboard> leaderboards { get; set; }
		public static IAbsRepo<Gamestat> gameStats { get; set; }
		public static IAbsRepo<NickNameId> nicknames { get; set; }

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

			try {

				return nicknames.GetItem(userID).UserNickName;
				//return new DataDataContext().getNickname(userID).ToList<getNicknameResult>().First().UserNickName;	
			} catch (Exception) {
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
				nicknames.AddItem(new NickNameId {
					UserId = userID,
					UserNickName = nickName
				});
				//new DataDataContext().addNickNameID(userID, nickName);
				return true;
			} catch (Exception) {
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

			try {
				return nicknames.UpdateItem(userID, new NickNameId {
					UserId = userID,
					UserNickName = nickName
				}) != null;
				//new DataDataContext().updateNickNameID(userID, nickName);
			} catch (Exception) {
				return false;
			}

		}

		/// <summary>
		/// Adds a record that this ID has started a game
		/// </summary>
		/// <param name="userID">User's ID</param>
		/// <returns>True, if there is no exception thrown.</returns>
		public static bool addTryID(int userID) {

			try {
                var stat = gameStats.GetItem(userID);
                if (stat != null) {
                    stat.GamesPlayed++;
                    gameStats.UpdateItem(userID, stat);
                } else {
                    gameStats.AddItem(new Gamestat {
                        GamesPlayed = 1,
						GamesWon = 0,
						DateStart = DateTime.Now
                	});
                }
				return true;
			} catch (Exception) {
				return false;
			}

		}

		/// <summary>
		/// Gets leaders for the given game mode. 
		/// </summary>
		/// <param name="mode">game mode</param>
		/// <returns>A sorted list of leaders.</returns>
		public static IEnumerable<Leader> getLeaderBoard(int mode) {

			try {
                return leaderboards.GetDbSet().Where(l => l.Mode == mode).OrderBy(l => l.Duration).Select(l => new Leader {
                    NickName = nicknames.GetDbSet().FirstOrDefault(g => g.UserId == l.UserId).UserNickName,
                    Duration = l.Duration.Value
            	});
                //return new DataDataContext().getLeaderBoard(mode).ToList<getLeaderBoardResult>();
            } catch (Exception) {
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
			try {
                var now = DateTime.Now;
                var start = gameStats.GetItem(userID).DateStart;
                var leaderboard = leaderboards.GetItems().FirstOrDefault(l => l.UserId == userID && l.Mode == mode);
				if (leaderboard != null) {
                    var duration = (now - start).Value.TotalMilliseconds;
                    if (leaderboard.Duration > duration) {
                        leaderboard.Duration = Convert.ToInt32(duration);
                        leaderboards.GetDbSet().Remove(leaderboards.GetDbSet().FirstOrDefault(l => l.UserId == userID && l.Mode == mode));
                        leaderboards.AddItem(leaderboard);
                    }
				} else {
                    leaderboards.AddItem(new Leaderboard {
						UserId = userID,
                        DateStart = start,
						DateEnd = now,
						Duration = Convert.ToInt32((now - start).Value.TotalMilliseconds),
						Mode = mode
                	});
                }
                
				return true;
			} catch (Exception) {
				return false;
			}
		}

		public static List<Move> runSolver(string field) {

			/*
				// Use ProcessStartInfo class
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.UseShellExecute = false;
				startInfo.FileName = HttpContext.Current.Server.MapPath("~/External Assemblies/Minesweeper Solver.png");
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				startInfo.Arguments = field;
				startInfo.RedirectStandardOutput = true;

				List<Move> moves = new List<Move>();

				try
				{
					// Start the process with the info we specified.
					// Call WaitForExit and then the using statement will close.
					using (Process exeProcess = Process.Start(startInfo))
					{
						string output = exeProcess.StandardOutput.ReadToEnd();
						exeProcess.WaitForExit();
						int count = exeProcess.ExitCode;

						string[] stringMoves = output.Split(' ');

						for (int i = 0; i < count; i++) {
							moves.Add(new Move(Convert.ToInt32(stringMoves[3 * i]), Convert.ToInt32(stringMoves[3 * i + 1]), Convert.ToInt32(stringMoves[3 * i + 2])));		
						}

					}
				}
				catch (Exception e)
				{
					Debug.WriteLine("Running .exe was unsuccessful: " + e.Message);
				}
			*/
			return SolverLauncher.runSolver(field);
		}

		private static string fieldToCLA(Place[][] field, Parameters param) {
			String result = "";

			result += param.width;
			result += " ";
			result += param.height;
			result += " ";
			result += param.numberOfMines;
			result += " ";
			for (int i = 0; i < field.Count(); i++) {
				for (int j = 0; j < field.First().Count(); j++) {
					result += field[i][j].number;
					result += " ";

					int state;
					if (field[i][j].isFlagged) {
						state = 1;
					} else if (field[i][j].isDiscovered) {
						state = 3;
					} else {
						state = 0;
					}

					result += state;
					result += " ";
				}
			}

			return result;
		}
	}
}