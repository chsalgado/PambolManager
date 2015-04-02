using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.API.Model
{
    internal static class TournamentExtensions
    {
        internal static TournamentDto ToTournamentDto(this Tournament tournament)
        {

            return new TournamentDto
            {
                Id = tournament.Id,
                FieldManagerId = tournament.FieldManagerId,
                TournamentName = tournament.TournamentName,
                TotalRounds = tournament.TotalRounds,
                MaxTeams = tournament.MaxTeams,
                BeginDate = tournament.BeginDate,
                EndDate = tournament.EndDate
            };
        }
    }
}
