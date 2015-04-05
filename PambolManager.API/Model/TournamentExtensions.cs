using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;

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
                EndDate = tournament.EndDate,
                Description = tournament.Description,
                TeamsQty = tournament.Teams == null ? 0 : tournament.Teams.Count
            };
        }
    }
}
