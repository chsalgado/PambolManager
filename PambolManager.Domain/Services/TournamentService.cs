using System.Linq;
using PambolManager.Domain.Entities;
using PambolManager.Domain.Entities.Core;

namespace PambolManager.Domain.Services
{
    public class TournamentService : ITournamentService
    {
        // Put here needed repositories for use cases/services to work
        private readonly IEntityRepository<Tournament> _tournamentRepository;

        // Constructor that takes all of the repositories as parameters
        public TournamentService(IEntityRepository<Tournament> tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
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
    }
}
