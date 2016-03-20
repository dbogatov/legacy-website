using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites
{
	public class PentagoGame
	{
		[Key]
		public int Id { get; set; }
		public string HostKey { get; set; }
		public string JoinKey { get; set; }
		public System.Nullable<System.DateTime> DateStarted { get; set; }
		public System.Nullable<System.DateTime> DateEnded { get; set; }
		public string GameCode { get; set; }
		public bool HostIsCross { get; set; }
		public System.Nullable<bool> IsGameEnded { get; set; }
		public string GameField { get; set; }
		public string HostName { get; set; }
		public string JoinName { get; set; }
		public bool IsHostTurn { get; set; }
		public string LastTurn { get; set; }
	}
}