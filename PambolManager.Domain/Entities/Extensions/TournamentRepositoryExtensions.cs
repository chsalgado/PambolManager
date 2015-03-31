using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.Domain.Entities
{
    public static class TournamentRepositoryExtensions
    {
        public static IQueryable<Tournament> GetTournamentsbyFieldManagerKey(
            this IEntityRepository<Tournament> tournamentRepository, string fieldManagerId)
        {
            return tournamentRepository.GetAll().Where(t => t.FieldManagerId == fieldManagerId);
        }
    }
}
