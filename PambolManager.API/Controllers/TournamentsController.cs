﻿using System;
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
using System.Net.Http;
using System.Net;
using PambolManager.API.Model.RequestModels;

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
        
        // GET api/Tournaments?page=1&take=20
        public async Task<PaginatedDto<TournamentDto>> GetTournamentsAsync(RequestCommand cmd)
        {
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            
            var tournaments = _tournamentService.GetTournaments(cmd.Page, cmd.Take, currentManager.Id);

            return tournaments.ToPaginatedDto(tournaments.Select(t => t.ToTournamentDto()));
        }

        // POST api/Tournaments
        public async Task<HttpResponseMessage> PostTournamentAsync(TournamentRequestModel requestModel)
        {
            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            requestModel.FieldManagerId = currentManager.Id;

            var createdTournamentResult = _tournamentService.AddTournament(requestModel.ToTournament());

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
            var tournament = _tournamentService.GetTournament(id);

            if(tournament == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);
            
            if(!_tournamentService.IsTournamentOwnedByUser(tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var updatedTournament = _tournamentService.UpdateTournament(requestModel.ToTournament(tournament));

            return updatedTournament.ToTournamentDto();
        }

        // DELETE api/Tournaments?id=00000000-0000-0000-0000-000000000000
        public async Task<HttpResponseMessage> DeleteTournamentAsync(Guid id)
        {
            var tournament = _tournamentService.GetTournament(id);

            if(tournament == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FieldManager currentManager = await _membershipService.FindUserByNameAsync(User.Identity.Name);

            if (!_tournamentService.IsTournamentOwnedByUser(tournament, currentManager.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var tournamentRemoveResult = _tournamentService.RemoveTournament(tournament);

            if (!tournamentRemoveResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
