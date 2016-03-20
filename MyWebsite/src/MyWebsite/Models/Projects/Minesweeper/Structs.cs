namespace MyWebsite.Models.Minesweeper
{

	/// <summary>
	/// Place structure
	/// </summary>
	public struct Place
	{
		public int number;
		public bool isMine;
		public bool isDiscovered;
		public bool isFlagged;
	}

	/// <summary>
	/// Change structure
	/// </summary>
	public struct Change
	{
		public Place place;
		public int x;
		public int y;

		public Change(int x, int y, Place place)
		{
			this.place = place;
			this.x = x;
			this.y = y;
		}
	}

	/// <summary>
	/// Game parameters
	/// </summary>
	public struct Parameters
	{
		public int width;
		public int height;
		public int numberOfMines;
		public int safePlaces;
		public int mode;

		public Parameters(int width, int height, int numberOfMines, int mode)
		{
			this.width = width;
			this.height = height;
			this.numberOfMines = numberOfMines;
			this.safePlaces = width * height - numberOfMines;
			this.mode = mode;
		}
	}

	/// <summary>
	/// Coordinates of a place
	/// </summary>
	public struct Coordinate
	{
		public Coordinate(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public int x;
		public int y;
	}

	public struct Move
	{
		public int x;
		public int y;
		public int action; // 0 - open, 1 - flag

		public Move(int x, int y, int action)
		{
			this.x = x;
			this.y = y;
			this.action = action;
		}
	}

	public struct Leader
	{
		public string NickName;
		public int Duration;
	}

}