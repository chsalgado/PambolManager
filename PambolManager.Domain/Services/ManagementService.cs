using System.Linq;
using PambolManager.Domain.Entities;
using PambolManager.Domain.Entities.Core;
using System;

namespace PambolManager.Domain.Services
{
    public class ManagementService : IManagementService
    {
        // Put here needed repositories for use cases/services to work
        private readonly IEntityRepository<Tournament> _tournamentRepository;
        private readonly IEntityRepository<Team> _teamRepository;
        private readonly IMembershipService _membershipService;

        // Constructor that takes all of the repositories as parameters
        public ManagementService(IEntityRepository<Tournament> tournamentRepository, IEntityRepository<Team> teamRepository)
        {
            _tournamentRepository = tournamentRepository;
            _teamRepository = teamRepository;
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

            if (tournament == null)
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
    }
}
