using PambolManager.API.Model.RequestModels;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class TournamentRequestModelsExtensions
    {
        internal static Tournament ToTournament(this TournamentRequestModel requestModel)
        {

            return new Tournament
            {
                TournamentName = requestModel.TournamentName,
                BeginDate = requestModel.BeginDate,
                EndDate = requestModel.EndDate,
                MaxTeams = requestModel.MaxTeams,
                Description = requestModel.Description
            };
        }

        internal static Tournament ToTournament(this TournamentUpdateRequestModel requestModel, Tournament existingTournament)
        {
            existingTournament.TournamentName = requestModel.TournamentName;
            existingTournament.MaxTeams = requestModel.MaxTeams;
            existingTournament.BeginDate = requestModel.BeginDate;
            existingTournament.EndDate = requestModel.EndDate;
            existingTournament.Description = requestModel.Description;

            return existingTournament;
        }
    }
}
