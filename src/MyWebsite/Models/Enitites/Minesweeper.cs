using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites
{
	[Table("Leaderboard")]
	public class Leaderboard
	{
		[Key]
		public int UserId { get; set; }
		public int Mode { get; set; }
		public int Duration { get; set; }

		public NickNameId NickNameId { get; set; }
	}

	[Table("Gamestat")]
	public class Gamestat
	{
		[Key]
		public int UserId { get; set; }
		public int GamesPlayed { get; set; }
		public int GamesWon { get; set; }
		public long MSStart { get; set; }
	}

	[Table("NickNameId")]
	public class NickNameId
	{
		[Key]
		public int UserId { get; set; }
		public string UserNickName { get; set; }
	}
}