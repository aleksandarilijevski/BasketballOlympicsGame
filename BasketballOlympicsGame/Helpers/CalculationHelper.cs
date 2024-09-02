using BasketballOlympicsGame.Models;
using BasketballOlympicsGame.Utilities;

namespace BasketballOlympicsGame.Helpers
{
    public static class CalculationHelper
    {
        public static Team CalculateWinChance(Team host, Team guest)
        {
            List<Team> teams = new List<Team>();

            double hostChance = (1.0 / host.FIBARanking) + CalculateTeamFrom(host.ISOCode);
            double guestChance = (1.0 / guest.FIBARanking) + CalculateTeamFrom(guest.ISOCode);

            double totalChance = hostChance + guestChance;

            double hostWinChance = hostChance / totalChance;
            double guestWinChance = guestChance / totalChance;

            Random random = new Random();
            double roll = random.NextDouble();

            if (roll < hostWinChance)
            {
                return host;
            }
            else
            {
                return guest;
            }
        }

        public static int GenerateRandomNumber(int from, int to)
        {
            Random random = Random.Shared;
            return random.Next(from, to);
        }

        public static double CalculateTeamFrom(string isoCode)
        {
            double form = 0.0;

            Dictionary<string, List<MatchResult>> matchResults = LoadDataHelper.LoadExibitions();
            List<MatchResult> teamMatches = matchResults[isoCode];

            foreach (MatchResult matchResult in teamMatches)
            {
                string[] result = matchResult.Result.Split('-');
                int pointDifference = int.Parse(result[0]) - int.Parse(result[1]);
                form += pointDifference;
            }

            return form;
        }
    }
}
