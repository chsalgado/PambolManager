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
    public class TournamentsController : ApiController
    {
        private readonly IManagementService _managementService;
        private readonly IMembershipService _membershipService;

        // Dependency injector injects IManagementService in here
        public TournamentsController(IManagementService managementService)
        {
            _managementService = managementService;
            _membershipService = new MembershipService();
        }
        
        // GET api/Tournaments?page=1&take=20
        public async Task<PaginatedDto<TournamentDto>> GetTournamentsAsync(RequestCommand cmd)
        {
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            
            var tournaments = _managementService.GetTournaments(cmd.Page, cmd.Take, currentManager.Id);

            return tournaments.ToPaginatedDto(tournaments.Select(t => t.ToTournamentDto()));
        }

        // POST api/Tournaments
        public async Task<HttpResponseMessage> PostTournamentAsync(TournamentRequestModel requestModel)
        {
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            requestModel.FieldManagerId = currentManager.Id;

            var createdTournamentResult = _managementService.AddTournament(requestModel.ToTournament());

            if(!createdTournamentResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, createdTournamentResult.Entity.ToTournamentDto());

            return response;
        }

        // PUT api/Tournaments?id=00000000-0000-0000-0000-000000000000
        public async Task<TournamentDto> PutTournamentAsync(Guid id, TournamentUpdateRequestModel requestModel)
        {
            var tournament = _managementService.GetTournament(id);

            if(tournament == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            
            if(!_managementService.IsTournamentOwnedByUser(tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var updatedTournament = _managementService.UpdateTournament(requestModel.ToTournament(tournament));

            return updatedTournament.ToTournamentDto();
        }

        // DELETE api/Tournaments?id=00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> DeleteTournamentAsync(Guid id)
        {
            var tournament = _managementService.GetTournament(id);

            if(tournament == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_managementService.IsTournamentOwnedByUser(tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var tournamentRemoveResult = _managementService.RemoveTournament(tournament);

            if (!tournamentRemoveResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
