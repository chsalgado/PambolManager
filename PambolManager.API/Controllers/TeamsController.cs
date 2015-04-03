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

namespace PambolManager.API.Controllers
{
    [Authorize]
    public class TeamsController : ApiController
    {
        private readonly IManagementService _managementService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects IManagementService in here
        public TeamsController(IManagementService managementService)
        {
            _managementService = managementService;
            _membershipService = new MembershipService();
        }

        // GET api/teams?tournamentId=00000000-0000-0000-0000-000000000000&page=1&take=20
        public async Task<PaginatedDto<TeamDto>> GetTeamsAsync(Guid tournamentId, RequestCommand cmd)
        {
            _managementService.CreateTournamentSchedule();
            
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

            var teams = _managementService.GetTeams(cmd.Page, cmd.Take, tournamentId);

            return teams.ToPaginatedDto(teams.Select(t => t.ToTeamDto()));
        }

        // POST api/Teams
        public async Task<HttpResponseMessage> PostTeamAsync(TeamRequestModel requestModel)
        {
            var tournament = _managementService.GetTournament(requestModel.TournamentId);

            if (tournament == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var createdTeamResult = _managementService.AddTeam(requestModel.ToTeam());

            if (!createdTeamResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, createdTeamResult.Entity.ToTeamDto());

            return response;
        }

        // PUT api/Teams?id=00000000-0000-0000-0000-000000000000
        public async Task<TeamDto> PutTeamAsync(Guid id, TeamUpdateRequestModel requestModel)
        {
            var team = _managementService.GetTeam(id);

            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(team.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var updatedTeam = _managementService.UpdateTeam(requestModel.ToTeam(team));

            return updatedTeam.ToTeamDto();
        }

        // DELETE api/Teams?id=00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> DeleteTournamentAsync(Guid id)
        {
            var team = _managementService.GetTeam(id);

            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(team.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var teamRemoveResult = _managementService.RemoveTeam(team);

            if (!teamRemoveResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
