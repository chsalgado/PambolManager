using PambolManager.API.Model;
using PambolManager.API.Model.Dtos;
using PambolManager.API.Model.RequestCommands;
using PambolManager.API.Model.RequestModels;
using PambolManager.Domain.Entities;
using PambolManager.Domain.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;

namespace PambolManager.API.Controllers
{
    [Authorize]
    public class RoundsController : ApiController
    {
        private readonly IManagementService _managementService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects IManagementService in here
        public RoundsController(IManagementService managementService)
        {
            _managementService = managementService;
            _membershipService = new MembershipService();
        }

        // POST api/Rounds?tournamentId = 00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> PostRoundsAsync(Guid tournamentId)
        {
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            var createdScheduleResult = _managementService.CreateTournamentSchedule(tournamentId);

            if (!createdScheduleResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            List<RoundDto> roundDtos = new List<RoundDto>();
            foreach (var round in createdScheduleResult.Entity)
            {
                roundDtos.Add(round.ToRoundDto());
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, roundDtos);

            return response;
        }

        // DELETE api/Tournaments?id=00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> DeleteRoundsAsync(Guid tournamentId)
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

            var scheduleRemoveResult = _managementService.RemoveRounds(tournamentId);

            if (!scheduleRemoveResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
