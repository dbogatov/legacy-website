using MyWebsite.Models.Projects.Pentago;

namespace MyWebsite.ViewModels.Projects.Pentago {
	
	public class CheckJoinViewModel
	{
		public string gameCode { get; set; }
	}
	
	public class TurnViewModel
	{
		public int x { get; set; }
		public int y { get; set; }
		public Cell mark { get; set; }
		public Quadrant field { get; set; }
		public RotDirection direction { get; set; }
	}
}