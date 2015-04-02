﻿using PambolManager.Domain.Entities;
using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.Domain.Services
{
    public interface IManagementService
    {
        //Tournaments
        PaginatedList<Tournament> GetTournaments(int pageIndex, int pageSize, string fielManagerId);
        OperationResult<Tournament> AddTournament(Tournament tournament);
        Tournament GetTournament(Guid id);
        Tournament UpdateTournament(Tournament tournament);
        bool IsTournamentOwnedByUser(Tournament tournament, string FieldManagerId);
        OperationResult RemoveTournament(Tournament tournament);

        //Teams
        PaginatedList<Team> GetTeams(int pageIndex, int pageSize, Guid tournamentId);
        OperationResult<Team> AddTeam(Team tournament);
        Team GetTeam(Guid id);
        Team UpdateTeam(Team team);
        OperationResult RemoveTeam(Team team);
    }
}