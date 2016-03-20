using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace MyWebsite.Models.Minesweeper
{
	/// <summary>
	/// This static class manges game field.
	/// </summary>
	public static class Field
	{

		public static ISession session { get; set; }

		/// <summary>
		/// Initializes field.
		/// </summary>
		/// <param name="noX">X-coordinate of place, which has to be the sea</param>
		/// <param name="noY">Y-coordinate of place, which has to be the sea</param>
		/// <param name="parameters">game parameters, such as height and width of a field</param>
		/// <returns>A two-dimensional array of Place structures.</returns>
		public static Place[][] initField(int noX, int noY, Parameters parameters)
		{

			Place[][] field = new Place[parameters.height][];

			// create new array (second dimension) for every element of the one-dimension array
			// put an empty Place there
			for (int i = 0; i < parameters.height; i++)
			{
				field[i] = new Place[parameters.width];
				for (int j = 0; j < parameters.width; j++)
				{
					field[i][j] = new Place();
				}
			}

			// seed random
			Random rand = new Random();
			int x, y;

			// set proper number of mines
			int radius;
			radius = parameters.safePlaces < 9 ? 1 : 2;

			for (int i = 0; i < parameters.numberOfMines; i++)
			{
				do
				{
					x = rand.Next(parameters.width);
					y = rand.Next(parameters.height);
				} while (field[y][x].isMine || (Math.Abs(y - noY) < radius && Math.Abs(x - noX) < radius));

				field[y][x].isMine = true;
			}

			// set numbers
			for (int i = 0; i < parameters.height; i++)
			{
				for (int j = 0; j < parameters.width; j++)
				{

					int sum = 0;
					if (i != 0 && j != 0)
						sum += field[i - 1][j - 1].isMine ? 1 : 0;

					if (i != 0)
						sum += field[i - 1][j].isMine ? 1 : 0;

					if (i != 0 && j < parameters.width - 1)
						sum += field[i - 1][j + 1].isMine ? 1 : 0;

					if (j < parameters.width - 1)
						sum += field[i][j + 1].isMine ? 1 : 0;

					if (i < parameters.height - 1 && j < parameters.width - 1)
						sum += field[i + 1][j + 1].isMine ? 1 : 0;

					if (i < parameters.height - 1)
						sum += field[i + 1][j].isMine ? 1 : 0;

					if (i < parameters.height - 1 && j != 0)
						sum += field[i + 1][j - 1].isMine ? 1 : 0;

					if (j != 0)
						sum += field[i][j - 1].isMine ? 1 : 0;

					field[i][j].number = sum;
				}
			}

			return field;
		}

		/// <summary>
		/// Opens a place in the field.
		/// Looses the game, if place is mine.
		/// Reveals sea, if place is sea.
		/// </summary>
		/// <param name="x">X-coordinate of place to open</param>
		/// <param name="y">Y-coordinate of place to open</param>
		/// <returns>A list of changes representing open places.</returns>
		public static List<Change> openPlace(int x, int y)
		{


			Place[][] field = session.GetObjectFromJson<Place[][]>("Field");// GetString("Field");
			List<Change> result = new List<Change>();

			// do actions only if place is not still opened
			if (!field[y][x].isDiscovered)
			{
				// if place is mine, report it and lose the game
				if (field[y][x].isMine)
				{
					result.Add(new Change(x, y, field[y][x]));
					Field.lose();
				}
				else if (field[y][x].number == 0)
				{ // if place is sea, reveal it
					result = revealSea(y, x, field, result);
				}
				else
				{ // otherwise, just discover it
					field[y][x].isDiscovered = true;
					result.Add(new Change(x, y, field[y][x]));
				}

				session.SetObjectAsJson("Field", field);

				// increase number of opened places
				int placesDiscovered = session.GetInt32("placesDiscovered").Value + result.Count(); // ["placesDiscovered"] + result.Count();

				session.SetInt32("placesDiscovered", placesDiscovered);
				//HttpContext.Current.Session["placesDiscovered"] = placesDiscovered;

				// check, whether player has won
				session.SetBoolean("won", Field.isWon());
				//HttpContext.Current.Session["won"] = Field.isWon();
			}

			return result;
		}

		/// <summary>
		/// Checks, whether the height and width are in the field.
		/// </summary>
		/// <param name="height">Y-coordinate</param>
		/// <param name="width">X-coordinate</param>
		/// <param name="field">two-dimensional array</param>
		/// <returns>True, if point is inside, false otherwise.</returns>
		public static bool checkForBounds(int height, int width, Place[][] field)
		{
			return (height >= 0 && height < field.Count() && width >= 0 && width < field.First().Count());
		}

		/// <summary>
		/// Recursive method, which reveals a sea.
		/// Function reveals given place and is executed on surrounding places.
		/// </summary>
		/// <param name="height">Y-coordinate</param>
		/// <param name="width">X-coordinate</param>
		/// <param name="field">two-dimensional array, representing a field</param>
		/// <param name="changes">changes, which have been previously made to the field</param>
		/// <returns>Changes, which have been made to the field.</returns>
		public static List<Change> revealSea(int height, int width, Place[][] field, List<Change> changes)
		{
			// First of all, check for bounds
			if (Field.checkForBounds(height, width, field))
			{
				// if place is not discovered
				if (!field[height][width].isDiscovered)
					// if place is a sea
					if (field[height][width].number == 0)
					{

						// open place, make proper change
						field[height][width].isDiscovered = true;
						changes.Add(new Change(width, height, field[height][width]));

						// invoke on surroundings
						changes = Field.revealSea(height + 1, width, field, changes);
						changes = Field.revealSea(height - 1, width, field, changes);
						changes = Field.revealSea(height, width + 1, field, changes);
						changes = Field.revealSea(height, width - 1, field, changes);
						changes = Field.revealSea(height + 1, width - 1, field, changes);
						changes = Field.revealSea(height - 1, width - 1, field, changes);
						changes = Field.revealSea(height + 1, width + 1, field, changes);
						changes = Field.revealSea(height - 1, width + 1, field, changes);

						return changes;
					}
					else
					{ // otherwise, just open it
						field[height][width].isDiscovered = true;
						changes.Add(new Change(width, height, field[height][width]));
						return changes;
					}
			}

			return changes;
		}

		/// <summary>
		/// Checks, whether user has won, and wins the game if necessary.
		/// </summary>
		/// <returns>True, if game is won, false otherwise.</returns>
		private static bool isWon()
		{
			//System.Diagnostics.Debug.Write("There are " + ((Parameters)HttpContext.Current.Session["Parameters"]).safePlaces + " safe places, and " + (int)HttpContext.Current.Session["placesDiscovered"] + "are discovered.\n");

			// compare number of opened places to the number of places that have to be opened AND player has not lost

			var safePlaces = session.GetObjectFromJson<Parameters>("Parameters").safePlaces;
			var discovered = session.GetInt32("placesDiscovered");
			var lost = session.GetBoolean("lost");
			if (safePlaces == discovered && !lost.Value)
			{
				//if (session.GetObjectFromJson<Parameters>("Parameters").safePlaces == session.GetInt32("placesDiscovered") && !Convert.ToBoolean(session.GetInt32("lost").Value)) {
				System.Diagnostics.Debug.Write("Win!");

				int userId = session.GetInt32("UserID").Value;
				int mode = session.GetObjectFromJson<Parameters>("Parameters").mode;

				//int mode = ((Parameters)HttpContext.Current.Session["Parameters"]).mode;

				// put victory to DB
				if (!session.GetBoolean("solverUsed").Value)
					new Thread(delegate ()
					{
						Utility.addAchievment(userId, mode);
					}).Start();

				session.SetBoolean("won", true);
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Looses the game.
		/// </summary>
		private static void lose()
		{
			session.SetBoolean("lost", true);
		}

		/// <summary>
		/// Returns all mines in the field.
		/// </summary>
		/// <returns>List of changes, where only mines are presented.</returns>
		public static List<Change> getAllMines()
		{
			Place[][] field = session.GetObjectFromJson<Place[][]>("Field");
			List<Change> result = new List<Change>();

			if (session.GetBoolean("lost").Value)
			{
				for (int i = 0; i < field.Count(); i++)
				{
					for (int j = 0; j < field.First().Count(); j++)
					{
						if (field[i][j].isMine)
						{
							result.Add(new Change(j, i, field[i][j]));
						}
					}
				}
			}
			else
			{
				session.SetBoolean("lost", true);
				//HttpContext.Current.Session["lost"] = true;
			}

			return result;
		}

		/// <summary>
		/// Opens a number of places
		/// </summary>
		/// <param name="places">array of coordinates of places to open</param>
		/// <returns>List of changes to the field</returns>
		public static List<Change> openPlaces(Coordinate[] places)
		{
			Place[][] field = session.GetObjectFromJson<Place[][]>("Field");
			List<Change> result = new List<Change>();

			foreach (Coordinate place in places)
			{
				result.AddRange(Field.openPlace(place.x, place.y));
			}

			return result;
		}

		public static List<Move> runSolver(string cla)
		{
			return Utility.runSolver(cla);
		}
	}
}