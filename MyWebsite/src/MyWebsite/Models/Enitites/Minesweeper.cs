using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites {

    [Table("LeaderBoards")]
    public class Leaderboard : AbsEntity {
        [Key]
		public int UserId { get; set; }
		public Nullable<int> Mode { get; set; }		
		public Nullable<DateTime> DateStart { get; set; }
		public Nullable<DateTime> DateEnd { get; set; }
		public Nullable<int> Duration { get; set; }
    }
	
	[Table("GameStats")]
    public class Gamestat : AbsEntity {
        [Key]
		public int UserId { get; set; }
		public Nullable<int> GamesPlayed { get; set; }
		public Nullable<int> GamesWon { get; set; }		
		public Nullable<DateTime> DateStart { get; set; }
    }
	
	[Table("NickNameID")]
    public class NickNameId : AbsEntity {
        [Key]
		public int UserId { get; set; }
		public string UserNickName { get; set; }
    }
}