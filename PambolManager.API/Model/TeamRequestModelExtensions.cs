using PambolManager.API.Model.RequestModels;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class TeamRequestModelsExtensions
    {
        internal static Team ToTeam(this TeamRequestModel requestModel)
        {
            return new Team
            {
                TournamentId = requestModel.TournamentId,
                TeamName = requestModel.TeamName
            };
        }

        internal static Team ToTeam(this TeamUpdateRequestModel requestModel, Team existingTeam)
        {
            existingTeam.TeamName = requestModel.TeamName;

            return existingTeam;
        }
    }
}
