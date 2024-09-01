using Newtonsoft.Json;

namespace BasketballOlympicsGame.Models
{
    public class Team
    {
        [JsonProperty("Team")]
        public string Name { get; set; }

        public string ISOCode { get; set; }

        public int FIBARanking { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Points { get; set; }

        public int ScoredPoints { get; set; }

        public int ReceivedPoints { get; set; }

        public int TotalScoredPoints { get; set; }

        public int TotalReceivedPoints { get; set; }

        public string GroupName { get; set; }

        public double Form { get; set; }

        public Team Clone()
        {
            return new Team
            {
                Name = this.Name,
                ISOCode = this.ISOCode,
                FIBARanking = this.FIBARanking,
                Wins = this.Wins,
                Losses = this.Losses,
                Points = this.Points,
                ScoredPoints = this.ScoredPoints,
                ReceivedPoints = this.ReceivedPoints,
                TotalReceivedPoints = this.TotalReceivedPoints,
                TotalScoredPoints = this.TotalScoredPoints,
                GroupName = this.GroupName,
                Form = this.Form,
            };
        }
    }
}
