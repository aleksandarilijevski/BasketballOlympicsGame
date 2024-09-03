using BasketballOlympicsGame.Helpers;
using BasketballOlympicsGame.Models;
using System;

namespace BasketballOlympicsGame.Helper
{
    public static class DisplayDataHelper
    {
        public static void DisplayHats(List<Team> hatD, List<Team> hatE, List<Team> hatF, List<Team> hatG)
        {
            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("Šešir D\n");
            Console.WriteLine(hatD[0].Name);
            Console.WriteLine(hatD[1].Name);

            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("Šešir E\n");
            Console.WriteLine(hatE[0].Name);
            Console.WriteLine(hatE[1].Name);

            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("Šešir F\n");
            Console.WriteLine(hatF[0].Name);
            Console.WriteLine(hatF[1].Name);

            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("Šešir G\n");
            Console.WriteLine(hatG[0].Name);
            Console.WriteLine(hatG[1].Name);
            Console.WriteLine("------------------------------------------------------");
        }

        public static void PressAnyKeyToContinue(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayPlasman(List<Team> plasman)
        {
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=PLASMAN=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            int ranking = 1;

            foreach (Team team in plasman)
            {
                Console.WriteLine(ranking + ".Name : " + team.Name);
                Console.WriteLine("FIBA Ranking : " + team.FIBARanking);
                Console.WriteLine("Wins : " + team.Wins);
                Console.WriteLine("Losses : " + team.Losses);
                Console.WriteLine("Points : " + team.Points);
                Console.WriteLine("Total scored points : " + team.TotalScoredPoints);
                Console.WriteLine("Total received points : " + team.TotalReceivedPoints);
                Console.WriteLine("Koš razlika : " + (team.TotalScoredPoints - team.TotalReceivedPoints));
                Console.WriteLine("--------------------------------------------------------------------------------");
                ranking++;
            }
        }

        public static void DisplayGroupPhase(List<Group> groups)
        {
            foreach (Group group in groups)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine($"Group {group.Name}");

                for (int i = 0; i < group.Teams.Count; i++)
                {
                    for (int j = i + 1; j < group.Teams.Count; j++)
                    {
                        Console.WriteLine($"{group.Teams[i].Name} vs {group.Teams[j].Name}");
                    }
                }
                Console.WriteLine("===================================================================================");
                Console.WriteLine();
            }
        }

        public static void DisplayTop3Teams(List<Team> top3Teams)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"{top3Teams[0].Name} (Zlato)");
            Console.WriteLine($"{top3Teams[1].Name} (Srebro)");
            Console.WriteLine($"{top3Teams[2].Name} (Bronza)");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayEliminationPhase(TeamHats teamHats)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Eliminaciona faza : \n");
            Console.WriteLine(teamHats.HatTeamD1.Name + " - " + teamHats.HatTeamG1.Name);
            Console.WriteLine(teamHats.HatTeamD2.Name + " - " + teamHats.HatTeamG2.Name);

            Console.WriteLine();

            Console.WriteLine(teamHats.HatTeamE1.Name + " - " + teamHats.HatTeamF1.Name);
            Console.WriteLine(teamHats.HatTeamE2.Name + " - " + teamHats.HatTeamF2.Name);
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayWinnerForThirdPlace(Semifinals semifinals)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Utakmica za treće mesto : \n");
            Console.WriteLine($"{semifinals.FirstSemifinals.Loser.Name} VS {semifinals.SecondSemifinals.Loser.Name}");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayWinnerForThirdPlaceMatchStatus(Tournament thirdPlace)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Utakmica za treće mesto : \n");
            Console.WriteLine($"{thirdPlace.Winner.Name} VS {thirdPlace.Loser.Name}\n Winner : {thirdPlace.Winner.Name}\n Score ({thirdPlace.Winner.ScoredPoints}:{thirdPlace.Loser.ScoredPoints})\n");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayQuarterfinals(TeamHats teamHats)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Četvrtfinale : \n");
            Console.WriteLine($"{teamHats.HatTeamD1.Name} VS {teamHats.HatTeamG1.Name}");
            Console.WriteLine($"{teamHats.HatTeamD2.Name} VS {teamHats.HatTeamG2.Name}");
            Console.WriteLine();
            Console.WriteLine($"{teamHats.HatTeamE1.Name} VS {teamHats.HatTeamF1.Name}");
            Console.WriteLine($"{teamHats.HatTeamE2.Name} VS {teamHats.HatTeamF2.Name}");

            Tournament tournamentDG1 = TournamentHelper.RandomMatchState(teamHats.HatTeamD1, teamHats.HatTeamG1);
            Tournament tournamentDG2 = TournamentHelper.RandomMatchState(teamHats.HatTeamD2, teamHats.HatTeamG2);
            Tournament tournamentEF1 = TournamentHelper.RandomMatchState(teamHats.HatTeamE1, teamHats.HatTeamF1);
            Tournament tournamentEF2 = TournamentHelper.RandomMatchState(teamHats.HatTeamE2, teamHats.HatTeamF2);

            List<Tournament> tournaments = new List<Tournament>
            {
                tournamentDG1,
                tournamentDG2,
                tournamentEF1,
                tournamentEF2
            };

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Polufinale - Kostur :");
            int randomTeam = CalculationHelper.GenerateRandomNumber(1, 2);

            if (randomTeam == 1)
            {
                Console.WriteLine("Pobednik prve utakmice VS pobednik iz treće utakmice");
                Console.WriteLine("Pobednik druge utakmice VS pobednik iz četvrte utakmice");
            }

            if (randomTeam == 2)
            {
                Console.WriteLine("Pobednik prve utakmice VS pobednik iz četvrte utakmice");
                Console.WriteLine("Pobednik druge utakmice VS pobednik iz treće utakmice");
            }

            Program.TournamentHistory.Remove(tournamentDG1);
            Program.TournamentHistory.Remove(tournamentDG2);
            Program.TournamentHistory.Remove(tournamentEF1);
            Program.TournamentHistory.Remove(tournamentEF2);
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayQuarterfinalsMatchStatus(Quarterfinals quarterfinals)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Četvrtfinale : \n");
            Console.WriteLine($"{quarterfinals.DG1.Winner.Name} VS {quarterfinals.DG1.Loser.Name}\n Winner : {quarterfinals.DG1.Winner.Name}\n Score ({quarterfinals.DG1.Winner.ScoredPoints}:{quarterfinals.DG1.Loser.ScoredPoints})\n");
            Console.WriteLine($"{quarterfinals.DG2.Winner.Name} VS {quarterfinals.DG2.Loser.Name}\n Winner : {quarterfinals.DG2.Winner.Name}\n Score ({quarterfinals.DG2.Winner.ScoredPoints}:{quarterfinals.DG2.Loser.ScoredPoints})\n");
            Console.WriteLine($"{quarterfinals.EF1.Winner.Name} VS {quarterfinals.EF1.Loser.Name}\n Winner : {quarterfinals.EF1.Winner.Name}\n Score ({quarterfinals.EF1.Winner.ScoredPoints}:{quarterfinals.EF1.Loser.ScoredPoints})\n");
            Console.WriteLine($"{quarterfinals.EF2.Winner.Name} VS {quarterfinals.EF2.Loser.Name}\n Winner : {quarterfinals.EF2.Winner.Name}\n Score ({quarterfinals.EF2.Winner.ScoredPoints}:{quarterfinals.EF2.Loser.ScoredPoints})\n");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplaySemifinals(Quarterfinals quarterfinals)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Polufinale :");
            Console.WriteLine($"{quarterfinals.DG1.Winner.Name} VS {quarterfinals.EF1.Winner.Name}");
            Console.WriteLine($"{quarterfinals.EF2.Winner.Name} VS {quarterfinals.DG2.Winner.Name}");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplaySemifinalsMatchStatus(Semifinals semifinals)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Polufinale :");
            Console.WriteLine($"{semifinals.FirstSemifinals.Winner.Name} VS {semifinals.FirstSemifinals.Loser.Name}\n Winner : {semifinals.FirstSemifinals.Winner.Name}\n Score ({semifinals.FirstSemifinals.Winner.ScoredPoints}:{semifinals.FirstSemifinals.Loser.ScoredPoints})\n");
            Console.WriteLine($"{semifinals.SecondSemifinals.Winner.Name} VS {semifinals.SecondSemifinals.Loser.Name}\n Winner : {semifinals.SecondSemifinals.Winner.Name}\n Score ({semifinals.SecondSemifinals.Winner.ScoredPoints}:{semifinals.SecondSemifinals.Loser.ScoredPoints})");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayFinals(Semifinals semifinals)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Finale : \n");
            Console.WriteLine($"{semifinals.FirstSemifinals.Winner.Name} VS {semifinals.SecondSemifinals.Winner.Name}");
            Console.WriteLine("------------------------------------------------------");
        }

        public static void DisplayFinalsMatchStatus(Tournament finalTournament)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Finale : \n");
            Console.WriteLine($"{finalTournament.Winner.Name} VS {finalTournament.Loser.Name}\n Winner : {finalTournament.Winner.Name}\n Score ({finalTournament.Winner.ScoredPoints}:{finalTournament.Loser.ScoredPoints})\n");
            Console.WriteLine("------------------------------------------------------");
        }
    }
}
