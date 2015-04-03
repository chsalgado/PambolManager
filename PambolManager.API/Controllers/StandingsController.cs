using PambolManager.API.Model;
using PambolManager.API.Model.Dtos;
using PambolManager.API.Model.RequestCommands;
using PambolManager.API.Model.RequestModels;
using PambolManager.Domain.Entities;
using PambolManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PambolManager.API.Controllers
{
    [Authorize]
    public class StandingsController : ApiController
    {
        private readonly IManagementService _managementService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects IManagementService in here
        public StandingsController(IManagementService managementService)
        {
            _managementService = managementService;
            _membershipService = new MembershipService();
        }

        // GET api/Standings?tournamentId=00000000-0000-0000-0000-000000000000
        public async Task<IEnumerable<StandingEntryDto>> GetStandingsAsync(Guid tournamentId)
        {
            var tournament = _managementService.GetTournament(tournamentId);

            if (tournament == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var standings = _managementService.GetStandings(tournamentId);

            List<StandingEntryDto> standingDtos = new List<StandingEntryDto>();
            foreach (var standingEntry in standings)
            {
                standingDtos.Add(standingEntry.ToStandingEntryDto());
            }

            return standingDtos;
        }
    }
}
