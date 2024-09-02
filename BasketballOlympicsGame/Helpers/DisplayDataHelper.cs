using BasketballOlympicsGame.Models;

namespace BasketballOlympicsGame.Helper
{
    public static class DisplayDataHelper
    {
        public static void DisplayHats(List<Team> hatD, List<Team> hatE, List<Team> hatF, List<Team> hatG)
        {
            Console.WriteLine("Sesiri : ");

            Console.WriteLine();

            Console.WriteLine("Sesir D");
            Console.WriteLine(hatD[0].Name);
            Console.WriteLine(hatD[1].Name);

            Console.WriteLine();

            Console.WriteLine("Sesir E");
            Console.WriteLine(hatE[0].Name);
            Console.WriteLine(hatE[1].Name);

            Console.WriteLine();

            Console.WriteLine("Sesir F");
            Console.WriteLine(hatF[0].Name);
            Console.WriteLine(hatF[1].Name);

            Console.WriteLine();

            Console.WriteLine("Sesir G");
            Console.WriteLine(hatG[0].Name);
            Console.WriteLine(hatG[1].Name);
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
                Console.WriteLine("Kos razlika : " + (team.TotalScoredPoints - team.TotalReceivedPoints));
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
            int ranking = 1;

            foreach (Team team in top3Teams)
            {
                Console.WriteLine($"{ranking}.{team.Name}");
                ranking++;
            }
        }

        public static void DisplayEliminationPhase(TeamHatsModel teamHatsModel)
        {
            Console.WriteLine("Eliminaciona faza : ");
            Console.WriteLine(teamHatsModel.HatTeamD1.Name + " - " + teamHatsModel.HatTeamG1.Name);
            Console.WriteLine(teamHatsModel.HatTeamD2.Name + " - " + teamHatsModel.HatTeamG2.Name);

            Console.WriteLine();

            Console.WriteLine(teamHatsModel.HatTeamE1.Name + " - " + teamHatsModel.HatTeamF1.Name);
            Console.WriteLine(teamHatsModel.HatTeamE2.Name + " - " + teamHatsModel.HatTeamF2.Name);
        }

        public static void DisplayWinnerForThirdPlace(Semifinals semifinals)
        {
            Console.WriteLine("Utakmica za trece mesto : \n");
            Console.WriteLine($"{semifinals.FirstSemifinals.Loser.Name} VS {semifinals.SecondSemifinals.Loser.Name}");
        }

        public static void DisplayWinnerForThirdPlaceMatchStatus(Tournament thirdPlace)
        {
            Console.WriteLine("Utakmica za trece mesto : \n");
            Console.WriteLine($"{thirdPlace.Winner.Name} VS {thirdPlace.Loser.Name}\n Winner : {thirdPlace.Winner.Name}\n Score ({thirdPlace.Winner.ScoredPoints}:{thirdPlace.Loser.ScoredPoints})\n");
        }

        public static void DisplayQuarterfinals(TeamHatsModel teamHatsModel)
        {
            Console.WriteLine("Cetvrtfinale : \n");
            Console.WriteLine($"{teamHatsModel.HatTeamD1.Name} VS {teamHatsModel.HatTeamG1.Name}");
            Console.WriteLine($"{teamHatsModel.HatTeamD2.Name} VS {teamHatsModel.HatTeamG2.Name}");
            Console.WriteLine();
            Console.WriteLine($"{teamHatsModel.HatTeamE1.Name} VS {teamHatsModel.HatTeamF1.Name}");
            Console.WriteLine($"{teamHatsModel.HatTeamE2.Name} VS {teamHatsModel.HatTeamF2.Name}");
        }

        public static void DisplayQuarterfinalsMatchStatus(Quarterfinals quarterfinals)
        {
            Console.WriteLine("Cetvrtfinale : \n");
            Console.WriteLine($"{quarterfinals.DG1.Winner.Name} VS {quarterfinals.DG1.Loser.Name}\n Winner : {quarterfinals.DG1.Winner.Name}\n Score ({quarterfinals.DG1.Winner.ScoredPoints}:{quarterfinals.DG1.Loser.ScoredPoints})\n");
            Console.WriteLine($"{quarterfinals.DG2.Winner.Name} VS {quarterfinals.DG2.Loser.Name}\n Winner : {quarterfinals.DG2.Winner.Name}\n Score ({quarterfinals.DG2.Winner.ScoredPoints}:{quarterfinals.DG2.Loser.ScoredPoints})\n");
            Console.WriteLine($"{quarterfinals.EF1.Winner.Name} VS {quarterfinals.EF1.Loser.Name}\n Winner : {quarterfinals.EF1.Winner.Name}\n Score ({quarterfinals.EF1.Winner.ScoredPoints}:{quarterfinals.EF1.Loser.ScoredPoints})\n");
            Console.WriteLine($"{quarterfinals.EF2.Winner.Name} VS {quarterfinals.EF2.Loser.Name}\n Winner : {quarterfinals.EF2.Winner.Name}\n Score ({quarterfinals.EF2.Winner.ScoredPoints}:{quarterfinals.EF2.Loser.ScoredPoints})\n");
        }

        public static void DisplaySemifinals(Quarterfinals quarterfinals)
        {
            Console.WriteLine("Polufinale :");
            Console.WriteLine($"{quarterfinals.DG1.Winner.Name} VS {quarterfinals.EF1.Winner.Name}");
            Console.WriteLine($"{quarterfinals.EF2.Winner.Name} VS {quarterfinals.DG2.Winner.Name}");
        }

        public static void DisplaySemifinalsMatchStatus(Semifinals semifinals)
        {
            Console.WriteLine("Polufinale :");
            Console.WriteLine($"{semifinals.FirstSemifinals.Winner.Name} VS {semifinals.FirstSemifinals.Loser.Name}\n Winner : {semifinals.FirstSemifinals.Winner.Name}\n Score ({semifinals.FirstSemifinals.Winner.ScoredPoints}:{semifinals.FirstSemifinals.Loser.ScoredPoints})\n");
            Console.WriteLine($"{semifinals.SecondSemifinals.Winner.Name} VS {semifinals.SecondSemifinals.Loser.Name}\n Winner : {semifinals.SecondSemifinals.Winner.Name}\n Score ({semifinals.SecondSemifinals.Winner.ScoredPoints}:{semifinals.SecondSemifinals.Loser.ScoredPoints})");
        }

        public static void DisplayFinals(Semifinals semifinals)
        {
            Console.WriteLine("Finale : \n");
            Console.WriteLine($"{semifinals.FirstSemifinals.Winner.Name} VS {semifinals.SecondSemifinals.Winner.Name}");
        }

        public static void DisplayFinalsMatchStatus(Tournament finalTournament)
        {
            Console.WriteLine("Finale : \n");
            Console.WriteLine($"{finalTournament.Winner.Name} VS {finalTournament.Loser.Name}\n Winner : {finalTournament.Winner.Name}\n Score ({finalTournament.Winner.ScoredPoints}:{finalTournament.Loser.ScoredPoints})\n");
        }
    }
}
