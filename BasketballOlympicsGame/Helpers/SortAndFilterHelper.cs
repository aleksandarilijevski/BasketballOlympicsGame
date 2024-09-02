using BasketballOlympicsGame.Helper;
using BasketballOlympicsGame.Models;

namespace BasketballOlympicsGame.Helpers
{
    public static class SortAndFilterHelper
    {
        public static List<Team> GetBestGroupTeams(List<Group> groups, List<Tournament> tournamentHistory, List<Team> sortedGroupsTeam)
        {
            TournamentHelper.ApplyTotalScores(groups, tournamentHistory);

            Remove3WorstTeams(groups);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            List<Team> allTeams = new List<Team>();

            foreach (Group group in groups)
            {
                allTeams.AddRange(group.Teams);
            }

            sortedGroupsTeam = allTeams
                .OrderByDescending(x => x.Points)
                .ToList();

            List<Team> groupA = sortedGroupsTeam
                .Where(x => x.GroupName == "A")
                .ToList();

            List<Team> groupB = sortedGroupsTeam
                .Where(x => x.GroupName == "B")
                .ToList();

            List<Team> groupC = sortedGroupsTeam
                .Where(x => x.GroupName == "C")
                .ToList();

            List<Team> prvoPlasirani = new List<Team> { groupA[0], groupB[0], groupC[0] };
            List<Team> drugoPlasirani = new List<Team> { groupA[1], groupB[1], groupC[1] };
            List<Team> trecePlasirani = new List<Team> { groupA[2], groupB[2], groupC[2] };

            prvoPlasirani = prvoPlasirani
                .OrderByDescending(x => x.Points)
                .OrderByDescending(x => x.TotalScoredPoints - x.TotalReceivedPoints)
                .OrderByDescending(x => x.TotalScoredPoints)
                .OrderBy(x => x.FIBARanking)
                .ToList();

            drugoPlasirani = drugoPlasirani
                .OrderByDescending(x => x.Points)
                .OrderByDescending(x => x.TotalScoredPoints - x.TotalReceivedPoints)
                .OrderByDescending(x => x.TotalScoredPoints)
                .OrderBy(x => x.FIBARanking)
                .ToList();

            trecePlasirani = trecePlasirani
                .OrderByDescending(x => x.Points)
                .OrderByDescending(x => x.TotalScoredPoints - x.TotalReceivedPoints)
                .OrderByDescending(x => x.TotalScoredPoints)
                .OrderBy(x => x.FIBARanking)
                .ToList();

            List<Team> plasman =
            [
                ..prvoPlasirani,
                ..drugoPlasirani,
                ..trecePlasirani
            ];

            return plasman;
        }

        public static void Remove3WorstTeams(List<Group> groups)
        {
            foreach (Group group in groups)
            {
                Team worstGroupTeam = group.Teams
                    .OrderBy(x => x.Points)
                    .ThenBy(x => x.ScoredPoints)
                    .Last();

                Team worstTeam = group.Teams
                    .FirstOrDefault(x => x.Name == worstGroupTeam.Name);

                Console.WriteLine(worstTeam.Name + " je diskvalifikovan/a!");

                group.Teams.Remove(worstTeam);
            }
        }
    }
}
