using PambolManager.Domain.Entities.Core;
using System;
using System.Linq;

namespace PambolManager.Domain.Entities
{
    public static class RoundRepositoryExtensions
    {
        public static IQueryable<Round> GetRoundsByTournamentId(
            this IEntityRepository<Round> roundRepository, Guid tournamentId)
        {
            return roundRepository.GetAll().Where(r => r.TournamentId == tournamentId);
        }
    }
}
