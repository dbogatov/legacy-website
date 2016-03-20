using System.Collections.Generic;

namespace MyWebsite.ViewModels.Projects.Minesweeper
{
	public class StartGameWrapper
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public int Mode { get; set; }
		public int MinesNumber { get; set; }
		public string UserName { get; set; }
	}

	public class OpenPlaceWrapper
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	public class GetLeaderBoardWrapper
	{
		public int Mode { get; set; }
	}

	public class RunSolverWrapper
	{
		public string Json { get; set; }
	}

	public class OpenPlacesWrapper
	{
		public IEnumerable<OpenPlaceWrapper> Places { get; set; }
	}
}