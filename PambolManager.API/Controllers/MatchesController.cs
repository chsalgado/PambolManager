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
    public class MatchesController : ApiController
    {
        private readonly IManagementService _managementService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects IManagementService in here
        public MatchesController(IManagementService managementService)
        {
            _managementService = managementService;
            _membershipService = new MembershipService();
        }

        // GET api/matches?tournamentId=00000000-0000-0000-0000-000000000000&roundNumber=5
        public async Task<IEnumerable<MatchDto>> GetMatchesAsync(Guid tournamentId, int roundNumber)
        {
            var round = _managementService.GetRoundByTournamentIdAndNumber(tournamentId, roundNumber);

            if (round == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(round.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var matches = _managementService.GetMatches(round.Id);

            List<MatchDto> matchDtos = new List<MatchDto>();

            foreach (var match in matches)
            {
                matchDtos.Add(match.ToMatchDto());
            }

            return matchDtos;
        }

        // PUT api/matches?id=00000000-0000-0000-0000-000000000000
        public async Task<MatchDto> PutMatchScoreAsync(Guid id, MatchUpdateRequestModel requestModel)
        {
            var match = _managementService.GetMatch(id);

            if (match == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(match.Round.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var updatedMatch = _managementService.UpdateMatch(requestModel.ToMatch(match));

            return updatedMatch.ToMatchDto();
        }

        // DELETE api/matches?id=00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> DeleteScoreAsync(Guid id)
        {
            var match = _managementService.GetMatch(id);

            if (match == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(match.Round.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            match.HomeGoals = 0;
            match.AwayGoals = 0;
            match.IsScoreSet = false;

            var updatedMatch = _managementService.UpdateMatch(match);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
