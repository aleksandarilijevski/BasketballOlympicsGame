using BasketballOlympicsGame.Helper;
using BasketballOlympicsGame.Helpers;
using BasketballOlympicsGame.Models;
using BasketballOlympicsGame.Utilities;
using System.Text;

namespace BasketballOlympicsGame
{
    public class Program
    {
        public static List<Group> Groups = new List<Group>();

        public static List<Tournament> TournamentHistory = new List<Tournament>();

        public static List<Team> SortedGroupsTeam = new List<Team>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            LoadDataHelper.LoadTeams(Groups);
            Console.WriteLine("Učitavanje podataka...");
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za nastavak");

            DisplayDataHelper.DisplayGroupPhase(Groups);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za prikaz rezultat ekipa koje nisu uspele da prodju dalju fazu utakmice");

            TournamentHelper.PlayTournamentInGroupPhase(Groups);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za prikaz plasmana");

            List<Team> plasman = SortAndFilterHelper.GetBestGroupTeams(Groups, TournamentHistory, SortedGroupsTeam);
            DisplayDataHelper.DisplayPlasman(plasman);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da saznate ko je izbačen iz žreb-a");

            plasman.RemoveAt(plasman.Count - 1);
            List<Team> top8Teams = plasman;

            Console.WriteLine($"9.{plasman[plasman.Count - 1].Name} je izbačen/a");
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da odete na prikaz šešira");

            HatGroup hatsModel = TournamentHelper.CreateHatGroups(plasman);

            DisplayDataHelper.DisplayHats(hatsModel.HatD, hatsModel.HatE, hatsModel.HatF, hatsModel.HatG);

            TeamHatsModel teamHatsModel = TournamentHelper.EliminationPhase(hatsModel);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da odete na prikaz eliminacione faze");

            DisplayDataHelper.DisplayEliminationPhase(teamHatsModel);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za nastavak");

            DisplayDataHelper.DisplayQuarterfinals(teamHatsModel);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da bi timovi iz četvrtfinala odigrali utakmice");

            TournamentHelper.CheckIfTeamsPlayedInGroupPhase(teamHatsModel, hatsModel);
            Quarterfinals quarterfinals = TournamentHelper.PlayQuarterfinals(teamHatsModel);

            DisplayDataHelper.DisplayQuarterfinalsMatchStatus(quarterfinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za nastavak");

            DisplayDataHelper.DisplaySemifinals(quarterfinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da bi timovi iz polufinala odigrali utakmice");

            Semifinals semifinals = TournamentHelper.PlaySemifinals(quarterfinals);

            DisplayDataHelper.DisplaySemifinalsMatchStatus(semifinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za nastavak");

            DisplayDataHelper.DisplayWinnerForThirdPlace(semifinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da bi timovi odigrali utakmicu za treće mesto");

            Tournament thirdPlace = TournamentHelper.PlayTournamentForThirdplace(semifinals);

            DisplayDataHelper.DisplayWinnerForThirdPlaceMatchStatus(thirdPlace);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme za nastavak");

            DisplayDataHelper.DisplayFinals(semifinals);
            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da bi timovi odigrali utakmicu za prvo mesto");

            Tournament finals = TournamentHelper.PlayFinals(semifinals);
            Tournament finalTournament = TournamentHelper.RandomMatchState(finals.Winner, finals.Loser);

            DisplayDataHelper.DisplayFinalsMatchStatus(finalTournament);

            List<Team> top3Teams = new List<Team>
            {
                finalTournament.Winner,
                finalTournament.Loser,
                thirdPlace.Winner
            };

            DisplayDataHelper.PressAnyKeyToContinue("Pritisnite bilo koje dugme da bi prikazali top 3 tima");
            DisplayDataHelper.DisplayTop3Teams(top3Teams);

            Environment.Exit(0);
        }
    }
}