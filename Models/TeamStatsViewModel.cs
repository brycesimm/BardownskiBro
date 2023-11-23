namespace BardownskiBro.Models
{
    public class TeamStatsViewModel
    {
        public List<PlayerStats>? skaterStats { get; set; }
        public List<PlayerStats>? goalieStats { get; set; }
        public List<Player>? forwardsList { get; set; }
        public List<Player>? defensemenList { get; set; }
        public List<Player>? goalieList {get; set; }
    }
}