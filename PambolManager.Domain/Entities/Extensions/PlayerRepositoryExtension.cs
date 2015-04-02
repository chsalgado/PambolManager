using PambolManager.Domain.Entities.Core;
using System;
using System.Linq;

namespace PambolManager.Domain.Entities
{
    public static class PlayerRepositoryExtensions
    {
        public static IQueryable<Player> GetPlayersByTeamId(
            this IEntityRepository<Player> playerRepository, Guid teamId)
        {
            return playerRepository.GetAll().Where(p => p.TeamId == teamId);
        }
    }
}