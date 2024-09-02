using BasketballOlympicsGame.Helper;
using BasketballOlympicsGame.Helpers;
using BasketballOlympicsGame.Models;
using BasketballOlympicsGame.Utilities;

namespace BasketballOlympicsGame
{
    public class Program
    {
        public static List<Group> Groups = new List<Group>();

        public static List<Tournament> TournamentHistory = new List<Tournament>();

        public static List<Team> SortedGroupsTeam = new List<Team>();

        static void Main(string[] args)
        {
            LoadDataHelper.LoadTeams(Groups);
            Console.WriteLine("Loading data...");
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            DisplayDataOnConsoleHelper.DisplayGroupPhase(Groups);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press enter to play tournaments of group phase");

            TournamentHelper.PlayTournamentInGroupPhase(Groups);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            List<Team> plasman = SortAndFilterHelper.GetBestGroupTeams(Groups, TournamentHistory, SortedGroupsTeam);
            DisplayDataOnConsoleHelper.DisplayPlasman(plasman);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            plasman.RemoveAt(plasman.Count - 1);
            List<Team> top8Teams = plasman;

            Console.WriteLine("Team 9 has been removed!");
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            HatsModel hatsModel = TournamentHelper.MakeHatsModel(plasman);

            DisplayDataOnConsoleHelper.DisplayHats(hatsModel.HatD, hatsModel.HatE, hatsModel.HatF, hatsModel.HatG);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            TeamHatsModel teamHatsModel = TournamentHelper.EliminationPhase(hatsModel);

            DisplayDataOnConsoleHelper.DisplayEliminationPhase(teamHatsModel);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            TournamentHelper.CheckIfTeamsPlayedInGroupPhase(teamHatsModel, hatsModel);
            Quarterfinals quarterfinals = TournamentHelper.PlayQuarterfinals(teamHatsModel);

            DisplayDataOnConsoleHelper.DisplayQuarterfinals(quarterfinals);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            Semifinals semifinals = TournamentHelper.PlaySemifinals(quarterfinals);
            DisplayDataOnConsoleHelper.DisplaySemifinals(semifinals);
            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press any key to continue...");

            Tournament thirdPlace = TournamentHelper.PlayTournamentForThirdplace(semifinals);
            DisplayDataOnConsoleHelper.DisplayWinnerForThirdPlace(thirdPlace);

            Tournament finals = TournamentHelper.PlayFinals(semifinals);
            Tournament finalTournament = TournamentHelper.RandomMatchState(finals.Winner, finals.Loser);

            DisplayDataOnConsoleHelper.DisplayFinals(finalTournament);

            List<Team> top3Teams = new List<Team>
            {
                finalTournament.Winner,
                finalTournament.Loser,
                thirdPlace.Winner
            };

            DisplayDataOnConsoleHelper.PressAnyKeyToContinue("Press enter to display top 3 teams...");
            DisplayDataOnConsoleHelper.DisplayTop3Teams(top3Teams);
        }
    }
}
