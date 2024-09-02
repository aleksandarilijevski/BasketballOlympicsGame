﻿using BasketballOlympicsGame.Enums;
using BasketballOlympicsGame.Models;

namespace BasketballOlympicsGame.Helpers
{
    public static class TournamentHelper
    {
        public static void ApplyTotalScores(List<Group> groups, List<Tournament> tournamentHistory)
        {
            foreach (Group group in groups)
            {
                foreach (Team team in group.Teams)
                {
                    List<Tournament> tournaments = tournamentHistory
                        .Where(x => x.Winner.Name == team.Name || x.Loser.Name == team.Name)
                        .ToList();

                    foreach (Tournament tournament in tournaments)
                    {
                        team.TotalScoredPoints += tournament.Winner.ScoredPoints;
                        team.TotalReceivedPoints += tournament.Winner.ReceivedPoints;
                    }
                }
            }
        }

        public static void PlayTournamentInGroupPhase(List<Group> groups)
        {
            foreach (Group group in groups)
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

        public static Tournament RandomMatchState(Team host, Team guest)
        {
            MatchState matchState;
            int randomMatchState = CalculationHelper.GenerateRandomNumber(1, 4);

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

            Program.TournamentHistory.Add(tournamentHistory);

            return tournamentHistory;
        }

        public static Tournament ApplyStats(Tournament tournament)
        {
            int scoredPoints = CalculationHelper.GenerateRandomNumber(80, 121);
            int receivedPoints = CalculationHelper.GenerateRandomNumber(80, 121);

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

            Team winner = CalculationHelper.CalculateWinChance(tournament.Winner, tournament.Loser);

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

        public static HatsModel MakeHatsModel(List<Team> plasman)
        {
            HatsModel hats = new HatsModel
            {
                HatD = new List<Team> { plasman[0], plasman[1] },
                HatE = new List<Team> { plasman[2], plasman[3] },
                HatF = new List<Team> { plasman[4], plasman[5] },
                HatG = new List<Team> { plasman[6], plasman[7] },
            };

            return hats;
        }

        public static TeamHatsModel EliminationPhase(HatsModel hatsModel)
        {
            Team hatDteam1 = hatsModel.HatD[CalculationHelper.GenerateRandomNumber(0, 1)];
            hatsModel.HatD.Remove(hatDteam1);
            Team hatDteam2 = hatsModel.HatD[0];

            Team hatGteam1 = hatsModel.HatG[CalculationHelper.GenerateRandomNumber(0, 1)];
            hatsModel.HatG.Remove(hatGteam1);
            Team hatGteam2 = hatsModel.HatG[0];

            Team hatEteam1 = hatsModel.HatE[CalculationHelper.GenerateRandomNumber(0, 1)];
            hatsModel.HatE.Remove(hatEteam1);
            Team hatEteam2 = hatsModel.HatE[0];

            Team hatFteam1 = hatsModel.HatF[CalculationHelper.GenerateRandomNumber(0, 1)];
            hatsModel.HatF.Remove(hatFteam1);
            Team hatFteam2 = hatsModel.HatF[0];

            TeamHatsModel teamHatsModel = new TeamHatsModel
            {
                HatTeamD1 = hatDteam1,
                HatTeamD2 = hatDteam2,
                HatTeamG1 = hatGteam1,
                HatTeamG2 = hatGteam2,
                HatTeamE1 = hatEteam1,
                HatTeamE2 = hatEteam2,
                HatTeamF1 = hatFteam1,
                HatTeamF2 = hatFteam2
            };

            return teamHatsModel;
        }

        public static bool IfTournamentPlayedInGroupPhase(Team teamA, Team teamB)
        {
            Tournament tournamentFind = Program.TournamentHistory.FirstOrDefault(x => (x.Winner.Name == teamA.Name && x.Loser.Name == teamB.Name) || (x.Winner.Name == teamB.Name && x.Loser.Name == teamA.Name));

            if (tournamentFind != null)
            {
                return true;
            }

            return false;
        }

        public static void SwapTeams(SwapTeamsModel swapTeamsModel)
        {
            bool alreadyPlayed = true;

            List<Team> allHatsTeams =
            [
                ..swapTeamsModel.HatD,
                ..swapTeamsModel.HatE,
                ..swapTeamsModel.HatF,
                ..swapTeamsModel.HatG
            ];

            do
            {
                int randomNumber = CalculationHelper.GenerateRandomNumber(0, allHatsTeams.Count - 1);
                swapTeamsModel.TeamB = allHatsTeams[randomNumber];

                alreadyPlayed = IfTournamentPlayedInGroupPhase(swapTeamsModel.TeamA, swapTeamsModel.TeamB);
            } while (alreadyPlayed == true);
        }

        public static void CheckIfTeamsPlayedInGroupPhase(TeamHatsModel teamHatsModel, HatsModel hatsModel)
        {
            bool dg1 = IfTournamentPlayedInGroupPhase(teamHatsModel.HatTeamD1, teamHatsModel.HatTeamG1);
            bool dg2 = IfTournamentPlayedInGroupPhase(teamHatsModel.HatTeamD2, teamHatsModel.HatTeamG2);
            bool ef1 = IfTournamentPlayedInGroupPhase(teamHatsModel.HatTeamE1, teamHatsModel.HatTeamF1);
            bool ef2 = IfTournamentPlayedInGroupPhase(teamHatsModel.HatTeamE2, teamHatsModel.HatTeamF2);

            SwapTeamsModel swapTeamsModel = new SwapTeamsModel
            {
                HatD = hatsModel.HatD,
                HatE = hatsModel.HatE,
                HatF = hatsModel.HatF,
                HatG = hatsModel.HatG
            };

            if (dg1)
            {
                swapTeamsModel.TeamA = teamHatsModel.HatTeamD1;
                swapTeamsModel.TeamB = teamHatsModel.HatTeamG1;
                SwapTeams(swapTeamsModel);
            }

            if (dg2)
            {
                swapTeamsModel.TeamA = teamHatsModel.HatTeamD2;
                swapTeamsModel.TeamB = teamHatsModel.HatTeamG2;
                SwapTeams(swapTeamsModel);
            }

            if (ef1)
            {
                swapTeamsModel.TeamA = teamHatsModel.HatTeamE1;
                swapTeamsModel.TeamB = teamHatsModel.HatTeamF1;
                SwapTeams(swapTeamsModel);
            }

            if (ef2)
            {
                swapTeamsModel.TeamA = teamHatsModel.HatTeamE2;
                swapTeamsModel.TeamB = teamHatsModel.HatTeamF2;
                SwapTeams(swapTeamsModel);
            }
        }

        public static Quarterfinals PlayQuarterfinals(TeamHatsModel teamHatsModel)
        {
            Tournament tournamentDG1 = RandomMatchState(teamHatsModel.HatTeamD1, teamHatsModel.HatTeamG1);
            Tournament tournamentDG2 = RandomMatchState(teamHatsModel.HatTeamD2, teamHatsModel.HatTeamG2);
            Tournament tournamentEF1 = RandomMatchState(teamHatsModel.HatTeamE1, teamHatsModel.HatTeamF1);
            Tournament tournamentEF2 = RandomMatchState(teamHatsModel.HatTeamE2, teamHatsModel.HatTeamF2);

            Quarterfinals quarterfinals = new Quarterfinals
            {
                DG1 = tournamentDG1,
                DG2 = tournamentDG2,
                EF1 = tournamentEF1,
                EF2 = tournamentEF2
            };

            return quarterfinals;
        }

        public static Semifinals PlaySemifinals(Quarterfinals quarterfinals)
        {
            Console.WriteLine("Pobednik iz prve utakmice VS pobednik iz trece utakmice");
            Console.WriteLine("Pobednik iz druge utakmice VS pobednik iz cetvrte utakmice");
            Console.WriteLine();

            Tournament firstSemiFinals = RandomMatchState(quarterfinals.DG1.Winner, quarterfinals.EF1.Winner);
            Tournament secondSemiFinals = RandomMatchState(quarterfinals.EF2.Winner, quarterfinals.DG2.Winner);

            Semifinals semifinals = new Semifinals
            {
                FirstSemifinals = firstSemiFinals,
                SecondSemifinals = secondSemiFinals
            };

            return semifinals;
        }

        public static Tournament PlayTournamentForThirdplace(Semifinals semifinals)
        {
            Tournament finals = RandomMatchState(semifinals.FirstSemifinals.Loser, semifinals.SecondSemifinals.Loser);
            return finals;
        }

        public static Tournament PlayFinals(Semifinals semifinals)
        {
            return RandomMatchState(semifinals.FirstSemifinals.Winner, semifinals.SecondSemifinals.Winner);
        }
    }
}
