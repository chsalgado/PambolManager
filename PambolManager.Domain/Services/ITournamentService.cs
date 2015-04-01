using PambolManager.Domain.Entities;
using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.Domain.Services
{
    public interface ITournamentService
    {
        PaginatedList<Tournament> GetTournaments(int pageIndex, int pageSize, string fielManagerId);
    }
}
