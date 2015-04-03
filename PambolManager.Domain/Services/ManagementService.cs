using System.Linq;
using PambolManager.Domain.Entities;
using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace PambolManager.Domain.Services
{
    public class ManagementService : IManagementService
    {
        // Put here needed repositories for use cases/services to work
        private readonly IEntityRepository<Tournament> _tournamentRepository;
        private readonly IEntityRepository<Team> _teamRepository;
        private readonly IEntityRepository<Player> _playerRepository;
        private readonly IEntityRepository<Round> _roundRepository;
        private readonly IEntityRepository<Match> _matchRepository;

        private readonly IMembershipService _membershipService;

        // Constructor that takes all of the repositories as parameters
        public ManagementService(
            IEntityRepository<Tournament> tournamentRepository, 
            IEntityRepository<Team> teamRepository,
            IEntityRepository<Player> playerRepository,
            IEntityRepository<Round> roundRepository,
            IEntityRepository<Match> matchRepository)
        {
            _tournamentRepository = tournamentRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _matchRepository = matchRepository;
            _membershipService = new MembershipService();
        }

        // Tournaments
        public PaginatedList<Tournament> GetTournaments(int pageIndex, int pageSize, string fieldManagerId)
        {
            var tournaments = _tournamentRepository
                .GetTournamentsbyFieldManagerKey(fieldManagerId)
                .OrderBy(t => t.TournamentName)
                .ToPaginatedList(pageIndex, pageSize);
            return tournaments;
        }

        public OperationResult<Tournament> AddTournament(Tournament tournament)
        {
            var currentManager = _membershipService.FindUserByNameAsync(tournament.FieldManagerId);
            
            if (currentManager == null)
            {
                return new OperationResult<Tournament>(false);
            }

            tournament.Id = Guid.NewGuid();
            tournament.TotalRounds = 0;

            _tournamentRepository.Add(tournament);
            _tournamentRepository.Save();

            return new OperationResult<Tournament>(true)
            {
                Entity = tournament
            };
        }

        public Tournament GetTournament(Guid id)
        {
            var tournament = _tournamentRepository.GetAll().FirstOrDefault(t => t.Id == id);
            
            return tournament;
        }

        public Tournament UpdateTournament(Tournament tournament)
        {
            _tournamentRepository.Edit(tournament);
            _tournamentRepository.Save();

            return tournament;
        }

        public bool IsTournamentOwnedByUser(Tournament tournament, string FieldManagerId)
        {
            return tournament.FieldManagerId == FieldManagerId;
        }

        public OperationResult RemoveTournament(Tournament tournament)
        {
            _tournamentRepository.DeleteGraph(tournament);
            _tournamentRepository.Save();

            return new OperationResult(true);
        }

        // Teams
        public PaginatedList<Team> GetTeams(int pageIndex, int pageSize, Guid tournamentId)
        {
            var teams = _teamRepository
                .GetTeamsByTournamentId(tournamentId)
                .OrderBy(t => t.TeamName)
                .ToPaginatedList(pageIndex, pageSize);

            return teams;
        }

        public OperationResult<Team> AddTeam(Team team)
        {
            var tournament = GetTournament(team.TournamentId);

            if (tournament == null || tournament.Teams.Count == tournament.MaxTeams)
            {
                return new OperationResult<Team>(false);
            }

            team.Id = Guid.NewGuid();

            // Just a placeHolder
            team.LogoPath = "/DefaultPath.jpg";

            _teamRepository.Add(team);
            _teamRepository.Save();

            return new OperationResult<Team>(true)
            {
                Entity = team
            };
        }

        public Team GetTeam(Guid id)
        {
            var team = _teamRepository.GetAll().FirstOrDefault(t => t.Id == id);

            return team;
        }

        public Team UpdateTeam(Team team)
        {
            _teamRepository.Edit(team);
            _teamRepository.Save();

            return team;
        }

        public OperationResult RemoveTeam(Team team)
        {
            _teamRepository.DeleteGraph(team);
            _teamRepository.Save();

            return new OperationResult(true);
        }

        // Players
        public PaginatedList<Player> GetPlayers(int pageIndex, int pageSize, Guid teamId)
        {
            var players = _playerRepository
                .GetPlayersByTeamId(teamId)
                .OrderBy(p => p.Name)
                .ToPaginatedList(pageIndex, pageSize);

            return players;
        }

        public OperationResult<Player> AddPlayer(Player player)
        {
            var team = GetTeam(player.TeamId);

            if (team == null)
            {
                return new OperationResult<Player>(false);
            }

            player.Id = Guid.NewGuid();

            _playerRepository.Add(player);
            _playerRepository.Save();

            return new OperationResult<Player>(true)
            {
                Entity = player
            };
        }

        public Player GetPlayer(Guid id)
        {
            var player = _playerRepository.GetAll().FirstOrDefault(p => p.Id == id);

            return player;
        }

        public Player UpdatePlayer(Player player)
        {
            _playerRepository.Edit(player);
            _playerRepository.Save();

            return player;
        }

        public OperationResult RemovePlayer(Player player)
        {
            _playerRepository.DeleteGraph(player);
            _playerRepository.Save();

            return new OperationResult(true);
        }

        // Rounds
        public OperationResult<IEnumerable<Round>> CreateTournamentSchedule(Guid tournamentId)
        {
            var tournament = GetTournament(tournamentId);
            var teams = _teamRepository
                .GetTeamsByTournamentId(tournamentId)
                .OrderBy(t => t.TeamName).ToList();
            
            if (teams.Count() % 2 != 0)
            {
                teams.Add(new Team { TeamName = "Bye" });
            }

            int totalTeams = teams.Count();

            int totalRounds = (totalTeams - 1);
            
            int halfSize = totalTeams / 2;

            var fixedTeam = teams.ElementAt(0);
            var movableTeams = teams.Skip(1).ToArray();

            int movableTeamsSize = movableTeams.Count();

            var existingRounds = _roundRepository.GetRoundsByTournamentId(tournamentId);

            foreach (var round in existingRounds)
            {
                _roundRepository.DeleteGraph(round);
            }

            // Create each of the rounds
            for (int round = 1; round <= totalRounds; round++)
            {
                var currentRound = new Round { Id = Guid.NewGuid(), RoundNumber = round, TournamentId = tournamentId };

                _roundRepository.Add(currentRound);

                int teamIdx = totalRounds - round;

                // Create match for fixed team
                var match = new Match { Id = Guid.NewGuid(), HomeTeamId = fixedTeam.Id, AwayTeamId = movableTeams[teamIdx].Id, RoundId = currentRound.Id };
                //var score = new 
                _matchRepository.Add(match);

                // Create remaining matches for current round
                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (idx - round) % movableTeamsSize;
                    int secondTeam = (movableTeamsSize - round -idx) % movableTeamsSize;

                    if (firstTeam < 0)
                    {
                        firstTeam += movableTeamsSize;
                    }

                    if (secondTeam < 0)
                    {
                        secondTeam += movableTeamsSize;
                    }

                    match = new Match { Id = Guid.NewGuid(), HomeTeamId = movableTeams[firstTeam].Id, AwayTeamId = movableTeams[secondTeam].Id, RoundId = currentRound.Id };
                    _matchRepository.Add(match);
                }
            }
            _roundRepository.Save();
            _matchRepository.Save();

            tournament.TotalRounds = totalRounds;
            UpdateTournament(tournament);

            return new OperationResult<IEnumerable<Round>>(true)
            {
                Entity = _roundRepository.GetRoundsByTournamentId(tournamentId).ToList()
            };
         }

        public OperationResult RemoveRounds(Guid tournamentId)
        {
            var existingRounds = _roundRepository.GetRoundsByTournamentId(tournamentId);
            var tournament = GetTournament(tournamentId);

            foreach (var round in existingRounds)
            {
                _roundRepository.DeleteGraph(round);
            }

            _roundRepository.Save();

            tournament.TotalRounds = 0;
            UpdateTournament(tournament);

            return new OperationResult(true);
        }

        public Round GetRoundByTournamentIdAndNumber(Guid tournamentId, int roundNumber)
        {
            var round = _roundRepository.GetRoundsByTournamentId(tournamentId).FirstOrDefault(r => r.RoundNumber == roundNumber);

            return round;
        }

        // Matches
        public IEnumerable<Match> GetMatches(Guid roundId)
        {
            var matches = _matchRepository
                .GetMatchesByRoundId(roundId)
                .OrderBy(p => p.HomeTeam.TeamName)
                .ToList();

            return matches;
        }

        public Match GetMatch(Guid matchId)
        {
            var match = _matchRepository.GetAll().FirstOrDefault(m => m.Id == matchId);

            return match;
        }

        public Match UpdateMatch(Match match)
        {
            _matchRepository.Edit(match);
            _matchRepository.Save();

            return match;
        }

        // StandingEntries
        public IEnumerable<StandingEntry> GetStandings(Guid tournamentId)
        {
            var teams = GetTournament(tournamentId).Teams;
            List<StandingEntry> standingEntries = new List<StandingEntry>();

            foreach(var team in teams)
            {
                var standingEntry = GetStandingEntry(team);
                standingEntries.Add(standingEntry);
            }

            List<StandingEntry> standings = standingEntries.OrderByDescending(s => s.Points)
                                            .ThenByDescending(s => s.GoalsDifference)
                                            .ThenByDescending(s => s.ScoredGoals)
                                            .ToList();

            return standings;
        }

        // Private helpers
        private StandingEntry GetStandingEntry(Team team)
        {
            string teamName = team.TeamName;

            int playedMatches = 0;
            int wonMatches = 0;
            int drawedMatches = 0;
            int lostMatches = 0;
            int scoredGoals = 0;
            int receivedGoals = 0;

            foreach (var match in team.HomeMatches)
            {
                if (match.IsScoreSet)
                {
                    playedMatches++;

                    if (match.HomeGoals > match.AwayGoals) wonMatches++;
                    else if (match.HomeGoals < match.AwayGoals) lostMatches++;
                    else drawedMatches++;

                    scoredGoals += match.HomeGoals;
                    receivedGoals += match.AwayGoals;
                }
            }

            foreach (var match in team.AwayMatches)
            {
                if (match.IsScoreSet)
                {
                    playedMatches++;

                    if (match.AwayGoals > match.HomeGoals) wonMatches++;
                    else if (match.AwayGoals < match.HomeGoals) lostMatches++;
                    else drawedMatches++;

                    scoredGoals += match.AwayGoals;
                    receivedGoals += match.HomeGoals;
                }
            }

            int goalsDifference = scoredGoals - receivedGoals;
            int points = (3 * wonMatches) + drawedMatches;

            return new StandingEntry
            {
                TeamName = teamName,
                PlayedMatches = playedMatches,
                WonMatches = wonMatches,
                DrawedMatches = drawedMatches,
                LostMatches = lostMatches,
                ScoredGoals = scoredGoals,
                ReceivedGoals = receivedGoals,
                GoalsDifference = goalsDifference,
                Points = points
            };
        }
    }
}
