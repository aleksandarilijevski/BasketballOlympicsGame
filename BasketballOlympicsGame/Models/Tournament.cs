namespace BasketballOlympicsGame.Models
{
    public class Tournament
    {
        public Team Winner { get; set; }

        public Team Loser { get; set; }

        public bool Surrender { get; set; } = false;

        public string Group { get; set; }
    }
}
