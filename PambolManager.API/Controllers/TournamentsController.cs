using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using PambolManager.Domain.Services;
using PambolManager.API.Model.Dtos;
using PambolManager.API.Model.RequestCommands;
using PambolManager.API.Model;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Controllers
{
    [Authorize]
    public class TournamentsController : ApiController
    {
        private readonly ITournamentService _tournamentService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects ITournamentService in here
        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
            _membershipService = new MembershipService();
        }

        public async Task<PaginatedDto<TournamentDto>> GetTournaments(RequestCommand cmd)
        {
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            
            var tournaments = _tournamentService.GetTournaments(cmd.Page, cmd.Take, currentManager.Id);

            return tournaments.ToPaginatedDto(tournaments.Select(t => t.ToTournamentDto()));
        }
    }
}
