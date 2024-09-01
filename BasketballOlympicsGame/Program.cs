using BasketballOlympicsGame.Enums;
using BasketballOlympicsGame.Models;
using Newtonsoft.Json;

namespace BasketballOlympicsGame
{
    public class Program
    {
        public static List<Group> Groups = new List<Group>();

        public static List<Tournament> TournamentHistory = new List<Tournament>();

        public static List<Team> SortedGroupsTeam = new List<Team>();

        static void Main(string[] args)
        {
            LoadTeams();
            PlayMatch();
            PressAnyKeyToContinue();

            PrintMatchHistory();
            PressAnyKeyToContinue();

            List<Team> plasman = GetBestGroupTeams();
            DisplayPlasman(plasman);
            PressAnyKeyToContinue();

            plasman.RemoveAt(plasman.Count - 1);
            List<Team> top8Teams = plasman;

            //Hats
            List<Team> hatD = new List<Team> { plasman[0], plasman[1] };
            List<Team> hatE = new List<Team> { plasman[2], plasman[3] };
            List<Team> hatF = new List<Team> { plasman[4], plasman[5] };
            List<Team> hatG = new List<Team> { plasman[6], plasman[7] };

            DisplayHats(hatD, hatE, hatF, hatG);
            PressAnyKeyToContinue();

            //Elimination phase
            Team hatDteam1 = hatD[GenerateRandomNumber(0, 1)];
            hatD.Remove(hatDteam1);
            Team hatDteam2 = hatD[0];

            Team hatGteam1 = hatG[GenerateRandomNumber(0, 1)];
            hatG.Remove(hatGteam1);
            Team hatGteam2 = hatG[0];

            Team hatEteam1 = hatE[GenerateRandomNumber(0, 1)];
            hatE.Remove(hatEteam1);
            Team hatEteam2 = hatE[0];

            Team hatFteam1 = hatF[GenerateRandomNumber(0, 1)];
            hatF.Remove(hatFteam1);
            Team hatFteam2 = hatF[0];

            Console.WriteLine("Eliminaciona faza : ");
            Console.WriteLine(hatDteam1.Name + " - " + hatGteam1.Name);
            Console.WriteLine(hatDteam2.Name + " - " + hatGteam2.Name);

            Console.WriteLine();

            Console.WriteLine(hatEteam1.Name + " - " + hatFteam1.Name);
            Console.WriteLine(hatEteam2.Name + " - " + hatFteam2.Name);
            PressAnyKeyToContinue();

            bool dg1 = IfTournamentPlayedInGroupPhase(hatDteam1, hatGteam1);
            bool dg2 = IfTournamentPlayedInGroupPhase(hatDteam2, hatGteam2);
            bool ef1 = IfTournamentPlayedInGroupPhase(hatEteam1, hatFteam1);
            bool ef2 = IfTournamentPlayedInGroupPhase(hatEteam2, hatFteam2);

            SwapTeamsModel swapTeamsModel = new SwapTeamsModel
            {
                HatD = hatD,
                HatE = hatE,
                HatF = hatF,
                HatG = hatG
            };

            if (dg1)
            {
                swapTeamsModel.TeamA = hatDteam1;
                swapTeamsModel.TeamB = hatGteam1;
                SwapTeams(swapTeamsModel);
            }

            if (dg2)
            {
                swapTeamsModel.TeamA = hatDteam2;
                swapTeamsModel.TeamB = hatGteam2;
                SwapTeams(swapTeamsModel);
            }

            if (ef1)
            {
                swapTeamsModel.TeamA = hatEteam1;
                swapTeamsModel.TeamB = hatFteam1;
                SwapTeams(swapTeamsModel);
            }

            if (ef2)
            {
                swapTeamsModel.TeamA = hatEteam2;
                swapTeamsModel.TeamB = hatFteam2;
                SwapTeams(swapTeamsModel);
            }

            Tournament tournamentDG1 = RandomMatchState(hatDteam1, hatGteam1);
            Tournament tournamentDG2 = RandomMatchState(hatDteam2, hatGteam2);
            Tournament tournamentEF1 = RandomMatchState(hatEteam1, hatFteam1);
            Tournament tournamentEF2 = RandomMatchState(hatEteam2, hatFteam2);

            Console.WriteLine("Cetvrtfinale : \n");
            Console.WriteLine($"{tournamentDG1.Winner.Name} VS {tournamentDG1.Loser.Name}\n Winner : {tournamentDG1.Winner.Name}\n Score ({tournamentDG1.Winner.ScoredPoints}:{tournamentDG1.Loser.ScoredPoints})\n");
            Console.WriteLine($"{tournamentDG2.Winner.Name} VS {tournamentDG2.Loser.Name}\n Winner : {tournamentDG2.Winner.Name}\n Score ({tournamentDG2.Winner.ScoredPoints}:{tournamentDG2.Loser.ScoredPoints})\n");
            Console.WriteLine($"{tournamentEF1.Winner.Name} VS {tournamentEF1.Loser.Name}\n Winner : {tournamentEF1.Winner.Name}\n Score ({tournamentEF1.Winner.ScoredPoints}:{tournamentEF1.Loser.ScoredPoints})\n");
            Console.WriteLine($"{tournamentEF2.Winner.Name} VS {tournamentEF2.Loser.Name}\n Winner : {tournamentEF2.Winner.Name}\n Score ({tournamentEF2.Winner.ScoredPoints}:{tournamentEF2.Loser.ScoredPoints})\n");

            PressAnyKeyToContinue();

            Console.WriteLine("Polufinale : \n");

            Tournament semiFinals = RandomMatchState(tournamentDG1.Winner, tournamentEF1.Winner);
            Tournament semiFinals2 = RandomMatchState(tournamentEF2.Winner, tournamentDG2.Winner);

            Console.WriteLine($"{semiFinals.Winner.Name} VS {semiFinals.Loser.Name}\n Winner : {semiFinals.Winner.Name}\n Score ({semiFinals.Winner.ScoredPoints}:{semiFinals.Loser.ScoredPoints})\n");
            Console.WriteLine($"{semiFinals2.Winner.Name} VS {semiFinals2.Loser.Name}\n Winner : {semiFinals2.Winner.Name}\n Score ({semiFinals2.Winner.ScoredPoints}:{semiFinals2.Loser.ScoredPoints})\n");

            PressAnyKeyToContinue();

            Tournament thirdPlace = RandomMatchState(semiFinals.Loser, semiFinals2.Loser);

            Console.WriteLine("Utakmica za trece mesto : \n");
            Console.WriteLine($"{thirdPlace.Winner.Name} VS {thirdPlace.Loser.Name}\n Winner : {thirdPlace.Winner.Name}\n Score ({thirdPlace.Winner.ScoredPoints}:{thirdPlace.Loser.ScoredPoints})\n");

            Tournament finals = RandomMatchState(semiFinals.Winner, semiFinals2.Winner);
            Tournament finalTeam = RandomMatchState(finals.Winner, finals.Loser);

            Console.WriteLine("Finale : \n");
            Console.WriteLine($"{finalTeam.Winner.Name} VS {finalTeam.Loser.Name}\n Winner : {finalTeam.Winner.Name}\n Score ({finalTeam.Winner.ScoredPoints}:{finalTeam.Loser.ScoredPoints})\n");

            List<Team> top3Teams = new List<Team>
            {
                finalTeam.Winner,
                finalTeam.Loser,
                thirdPlace.Winner
            };

            DisplayTop3Teams(top3Teams);
        }

        public static bool IfTournamentPlayedInGroupPhase(Team teamA, Team teamB)
        {
            Tournament tournamentHistory = TournamentHistory.FirstOrDefault(x => (x.Winner.Name == teamA.Name && x.Loser.Name == teamB.Name) || (x.Winner.Name == teamB.Name && x.Loser.Name == teamA.Name));

            if (tournamentHistory != null)
            {
                return true;
            }

            return false;
        }

        public static void SwapTeams(SwapTeamsModel swapTeamsModel)
        {
            bool alreadyPlayed = true;

            List<Team> allHatsTeams = new List<Team>();
            allHatsTeams.AddRange(swapTeamsModel.HatD);
            allHatsTeams.AddRange(swapTeamsModel.HatE);
            allHatsTeams.AddRange(swapTeamsModel.HatF);
            allHatsTeams.AddRange(swapTeamsModel.HatG);

            do
            {
                int randomNumber = GenerateRandomNumber(0, allHatsTeams.Count - 1);
                swapTeamsModel.TeamB = allHatsTeams[randomNumber];

                alreadyPlayed = IfTournamentPlayedInGroupPhase(swapTeamsModel.TeamA, swapTeamsModel.TeamB);
            } while (alreadyPlayed == true);
        }

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

        public static void DisplayTop3Teams(List<Team> top3Teams)
        {
            int ranking = 1;

            foreach (Team team in top3Teams)
            {
                Console.WriteLine($"{ranking}.{team.Name}");
                ranking++;
            }
        }

        public static void DisplayPlasman(List<Team> plasman)
        {
            Console.WriteLine("----------------------PLASMAN-----------------------");

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
                Console.WriteLine("------------------------------------------------------");
                ranking++;
            }
        }

        public static void PlayMatch()
        {
            foreach (Group group in Groups)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine($"Group {group.Name}");

                for (int i = 0; i < group.Teams.Count; i++)
                {
                    for (int j = i + 1; j < group.Teams.Count; j++)
                    {
                        Tournament tournament = RandomMatchState(group.Teams[i], group.Teams[j]);
                        tournament.Group = group.Name;

                        Console.WriteLine($"{tournament.Winner.Name} vs {tournament.Loser.Name} ({tournament.Winner.ScoredPoints}:{tournament.Loser.ScoredPoints})");
                    }
                }
                Console.WriteLine("===================================================================================");
                Console.WriteLine();
            }
        }

        public static List<Team> GetBestGroupTeams()
        {
            //Calculate total scored and received points in all 3 matches
            foreach (Group group in Groups)
            {
                foreach (Team team in group.Teams)
                {
                    List<Tournament> tournaments = TournamentHistory
                        .Where(x => x.Winner.Name == team.Name || x.Loser.Name == team.Name)
                        .ToList();

                    foreach (Tournament tournament in tournaments)
                    {
                        team.TotalScoredPoints += tournament.Winner.ScoredPoints;
                        team.TotalReceivedPoints += tournament.Winner.ReceivedPoints;
                    }
                }
            }

            //Remove worst 3 teams
            foreach (Group group in Groups)
            {
                Team worstGroupTeam = group.Teams
                    .OrderBy(x => x.Points)
                    .ThenBy(x => x.ScoredPoints)
                    .Last();

                Team worstTeam = group.Teams
                    .FirstOrDefault(x => x.Name == worstGroupTeam.Name);

                group.Teams.Remove(worstTeam);
            }

            //Sort teams
            List<Team> allTeams = new List<Team>();

            foreach (Group group in Groups)
            {
                allTeams.AddRange(group.Teams);
            }

            SortedGroupsTeam = allTeams
                .OrderByDescending(x => x.Points)
                .ToList();

            //Give the teams rankings
            List<Team> groupA = SortedGroupsTeam
                .Where(x => x.GroupName == "A")
                .ToList();

            List<Team> groupB = SortedGroupsTeam
                .Where(x => x.GroupName == "B")
                .ToList();

            List<Team> groupC = SortedGroupsTeam
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

            List<Team> plasman = new List<Team>();
            plasman.AddRange(prvoPlasirani);
            plasman.AddRange(drugoPlasirani);
            plasman.AddRange(trecePlasirani);

            foreach (Team team in plasman)
            {
                Console.WriteLine($"Group : {team.GroupName}");
                Console.WriteLine("Name : " + team.Name);
                Console.WriteLine("FIBA Ranking : " + team.FIBARanking);
                Console.WriteLine("Wins : " + team.Wins);
                Console.WriteLine("Losses : " + team.Losses);
                Console.WriteLine("Points : " + team.Points);
                Console.WriteLine("Total scored points : " + team.TotalScoredPoints);
                Console.WriteLine("Total received points : " + team.TotalReceivedPoints);
                Console.WriteLine("------------------------------------------------------");
            }

            return plasman;
        }

        public static void PrintMatchHistory()
        {
            Console.WriteLine("-------------------------------------------------------------------");

            foreach (Tournament tournament in TournamentHistory)
            {
                Console.WriteLine($"{tournament.Winner.Name} VS {tournament.Loser.Name} ({tournament.Winner.ScoredPoints} : {tournament.Loser.ScoredPoints})");
            }
        }

        public static double CalculateTeamFrom(string isoCode)
        {
            double form = 0.0;

            Dictionary<string, List<MatchResult>> matchResults = LoadExibitions();
            List<MatchResult> teamMatches = matchResults[isoCode];

            foreach (MatchResult matchResult in teamMatches)
            {
                string[] result = matchResult.Result.Split('-');
                int pointDifference = int.Parse(result[0]) - int.Parse(result[1]);
                form += pointDifference;
            }

            return form;
        }

        public static Team CalculateWinChance(Team host, Team guest)
        {
            List<Team> teams = new List<Team>();

            double hostChance = (1.0 / host.FIBARanking) + host.Form;
            double guestChance = (1.0 / guest.FIBARanking) + guest.Form;

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

        public static Tournament RandomMatchState(Team host, Team guest)
        {
            MatchState matchState;
            int randomMatchState = GenerateRandomNumber(1, 4);

            matchState = (MatchState)randomMatchState;

            Tournament tournamentHistory = null;
            Tournament tournament = null;

            switch (matchState)
            {
                case MatchState.Win:
                    tournamentHistory = new Tournament { Winner = host.Clone(), Loser = guest.Clone() };
                    tournament = new Tournament { Winner = host, Loser = guest };

                    tournamentHistory = ApplyStats(tournamentHistory);
                    ApplyStats(tournament);
                    break;

                case MatchState.Lose:
                    tournamentHistory = new Tournament { Winner = guest.Clone(), Loser = host.Clone() };
                    tournament = new Tournament { Winner = guest, Loser = host };

                    tournamentHistory = ApplyStats(tournamentHistory);
                    ApplyStats(tournament);
                    break;

                case MatchState.Surrend:
                    tournamentHistory = new Tournament { Winner = host.Clone(), Loser = guest.Clone(), Surrender = true };
                    tournament = new Tournament { Winner = host, Loser = guest, Surrender = true };

                    tournamentHistory = ApplyStats(tournamentHistory);
                    ApplyStats(tournament);
                    break;
            }

            if (tournamentHistory.Loser.ScoredPoints > tournamentHistory.Winner.ScoredPoints)
            {
                Team teamTemp = tournamentHistory.Winner;

                tournamentHistory.Winner = tournamentHistory.Loser;
                tournamentHistory.Loser = teamTemp;

                tournament.Winner = tournament.Loser;
                tournament.Loser = teamTemp;

            }

            TournamentHistory.Add(tournamentHistory);

            return tournamentHistory;
        }

        public static Tournament ApplyStats(Tournament tournament)
        {
            int scoredPoints = GenerateRandomNumber(80, 121);
            int receivedPoints = GenerateRandomNumber(80, 121);

            if (tournament.Surrender)
            {
                tournament.Winner.Wins += 1;
                tournament.Winner.Points += 2;
                tournament.Winner.ReceivedPoints = receivedPoints;
                tournament.Winner.ScoredPoints = scoredPoints;

                tournament.Loser.Losses += 1;
                tournament.Loser.ReceivedPoints = tournament.Winner.ScoredPoints;
                tournament.Loser.ScoredPoints = tournament.Winner.ReceivedPoints;

                return tournament;
            }

            Team winner = CalculateWinChance(tournament.Winner, tournament.Loser);

            if (tournament.Winner != winner)
            {
                Team loser = tournament.Winner;

                tournament.Winner = winner;
                tournament.Loser = loser;
            }

            tournament.Winner.Wins += 1;
            tournament.Winner.Points += 2;
            tournament.Winner.ReceivedPoints = receivedPoints;
            tournament.Winner.ScoredPoints = scoredPoints;

            tournament.Loser.Losses += 1;
            tournament.Loser.Points += 1;
            tournament.Loser.ReceivedPoints = tournament.Winner.ScoredPoints;
            tournament.Loser.ScoredPoints = tournament.Winner.ReceivedPoints;

            return tournament;
        }

        public static int GenerateRandomNumber(int from, int to)
        {
            Random random = Random.Shared;
            return random.Next(from, to);
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public static Dictionary<string, List<MatchResult>> LoadExibitions()
        {
            string json = File.ReadAllText("exibitions.json");
            return JsonConvert.DeserializeObject<Dictionary<string, List<MatchResult>>>(json);
        }

        public static void LoadTeams()
        {
            string json = File.ReadAllText("groups.json");
            Dictionary<string, List<Team>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<Team>>>(json);

            foreach (KeyValuePair<string, List<Team>> keyValuePair in dictionary)
            {
                Group group = new Group
                {
                    Name = keyValuePair.Key,
                    Teams = keyValuePair.Value
                };

                Groups.Add(group);
            }

            foreach (Group group in Groups)
            {
                foreach (Team team in group.Teams)
                {
                    team.GroupName = group.Name;
                }
            }
        }
    }
}
