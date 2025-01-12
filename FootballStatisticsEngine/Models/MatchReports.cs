using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatisticsEngine.Models
{
    public class MatchReports
    {
        public int MatchID { get; set; }
        public DateTime? MatchDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public string? Competition { get; set; }
        public string? Round { get; set; }
        public string? Day { get; set; }
        public string? Venue { get; set; }
        public string? Result { get; set; }
        public int? GoalsFor { get; set; }
        public int? GoalsAgainst { get; set; }
        public string? Opponent { get; set; }
        public int? Possession { get; set; }
        public int? Attendance { get; set; }
        public string? Captain { get; set; }
        public string? Formation { get; set; }
        public string? OppFormation { get; set; }
        public string? Referee { get; set; }
        public string? MatchReport { get; set; }
        public string? Notes { get; set; }
    }
}
