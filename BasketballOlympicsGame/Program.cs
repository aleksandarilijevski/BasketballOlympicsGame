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
            Console.WriteLine("Ucitavanje podataka...");
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            DisplayDataHelper.DisplayGroupPhase(Groups);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite enter da se utakmice iz grupne faze odigraju...");

            TournamentHelper.PlayTournamentInGroupPhase(Groups);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            List<Team> plasman = SortAndFilterHelper.GetBestGroupTeams(Groups, TournamentHistory, SortedGroupsTeam);
            DisplayDataHelper.DisplayPlasman(plasman);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            plasman.RemoveAt(plasman.Count - 1);
            List<Team> top8Teams = plasman;

            Console.WriteLine($"9.{plasman[plasman.Count - 1].Name} je uklonjen/a");
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            HatsModel hatsModel = TournamentHelper.MakeHatsModel(plasman);

            DisplayDataHelper.DisplayHats(hatsModel.HatD, hatsModel.HatE, hatsModel.HatF, hatsModel.HatG);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            TeamHatsModel teamHatsModel = TournamentHelper.EliminationPhase(hatsModel);

            DisplayDataHelper.DisplayEliminationPhase(teamHatsModel);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            DisplayDataHelper.DisplayQuarterfinals(teamHatsModel);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite enter da bi timovi iz cetvrtfinala odigrali utakmice...");

            TournamentHelper.CheckIfTeamsPlayedInGroupPhase(teamHatsModel, hatsModel);
            Quarterfinals quarterfinals = TournamentHelper.PlayQuarterfinals(teamHatsModel);

            DisplayDataHelper.DisplayQuarterfinalsMatchStatus(quarterfinals);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            DisplayDataHelper.DisplaySemifinals(quarterfinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite enter da bi timovi iz polufinala odigrali utakmice...");

            Semifinals semifinals = TournamentHelper.PlaySemifinals(quarterfinals);

            DisplayDataHelper.DisplaySemifinalsMatchStatus(semifinals);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            DisplayDataHelper.DisplayWinnerForThirdPlace(semifinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite enter da bi timovi odigrali utakmicu za trece mesto...");

            Tournament thirdPlace = TournamentHelper.PlayTournamentForThirdplace(semifinals);

            DisplayDataHelper.DisplayWinnerForThirdPlaceMatchStatus(thirdPlace);
            DisplayDataHelper.PressAnyKeyToContinue("Press any key to continue...");

            DisplayDataHelper.DisplayFinals(semifinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite enter da bi timovi odigrali utakmicu za prvo mesto...");

            Tournament finals = TournamentHelper.PlayFinals(semifinals);
            Tournament finalTournament = TournamentHelper.RandomMatchState(finals.Winner, finals.Loser);

            DisplayDataHelper.DisplayFinalsMatchStatus(finalTournament);

            List<Team> top3Teams = new List<Team>
            {
                finalTournament.Winner,
                finalTournament.Loser,
                thirdPlace.Winner
            };

            DisplayDataHelper.PressAnyKeyToContinue("Press enter to display top 3 teams...");
            DisplayDataHelper.DisplayTop3Teams(top3Teams);
        }
    }
}
