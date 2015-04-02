using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.Domain.Entities
{
    public static class TeamRepositoryExtensions
    {
        public static IQueryable<Team> GetTeamsByTournamentId(
            this IEntityRepository<Team> teamRepository, Guid tournamentId)
        {
            return teamRepository.GetAll().Where(t => t.TournamentId == tournamentId);
        }
    }
}
