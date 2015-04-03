using PambolManager.Domain.Entities.Core;
using System;
using System.Linq;

namespace PambolManager.Domain.Entities
{
    public static class MatchRepositoryExtensions
    {
        public static IQueryable<Match> GetMatchesByRoundId(
            this IEntityRepository<Match> matchRepository, Guid roundId)
        {
            return matchRepository.GetAll().Where(m => m.RoundId == roundId);
        }
    }
}
