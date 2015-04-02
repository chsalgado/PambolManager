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
        private readonly IMembershipService _membershipService;

        // Constructor that takes all of the repositories as parameters
        public ManagementService(IEntityRepository<Tournament> tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
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
    }
}
