using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Enitites
{

    [Table("LeaderBoards")]
    public class Leaderboard
    {
        [Key]
        public int UserId { get; set; }
        public int Mode { get; set; }
        public int Duration { get; set; }

        public NickNameId NickNameId { get; set; }
    }

    [Table("GameStats")]
    public class Gamestat
    {
        [Key]
        public int UserId { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public long MSStart { get; set; }
    }

    [Table("NickNameID")]
    public class NickNameId
    {
        [Key]
        public int UserId { get; set; }
        public string UserNickName { get; set; }
    }
}