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
    public class PlayersController : ApiController
    {
        private readonly IManagementService _managementService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects IManagementService in here
        public PlayersController(IManagementService managementService)
        {
            _managementService = managementService;
            _membershipService = new MembershipService();
        }

        // GET api/player?teamId=00000000-0000-0000-0000-000000000000&page=1&take=20
        public async Task<PaginatedDto<PlayerDto>> GetPlayersAsync(Guid teamId, RequestCommand cmd)
        {
            var team = _managementService.GetTeam(teamId);

            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(team.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var players = _managementService.GetPlayers(cmd.Page, cmd.Take, teamId);

            return players.ToPaginatedDto(players.Select(p => p.ToPlayerDto()));
        }

        // POST api/players
        public async Task<HttpResponseMessage> PostPlayerAsync(PlayerRequestModel requestModel)
        {
            var team = _managementService.GetTeam(requestModel.TeamId);

            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(team.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var createdPlayerResult = _managementService.AddPlayer(requestModel.ToPlayer());

            if (!createdPlayerResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, createdPlayerResult.Entity.ToPlayerDto());

            return response;
        }

        // PUT api/players?id=00000000-0000-0000-0000-000000000000
        public async Task<PlayerDto> PutPlayerAsync(Guid id, PlayerUpdateRequestModel requestModel)
        {
            var player = _managementService.GetPlayer(id);

            if (player == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(player.Team.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var updatedPlayer = _managementService.UpdatePlayer(requestModel.ToPlayer(player));

            return updatedPlayer.ToPlayerDto();
        }

        // DELETE api/players?id=00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> DeletePlayerAsync(Guid id)
        {
            var player = _managementService.GetPlayer(id);

            if (player == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(player.Team.Tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var playerRemoveResult = _managementService.RemovePlayer(player);

            if (!playerRemoveResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
