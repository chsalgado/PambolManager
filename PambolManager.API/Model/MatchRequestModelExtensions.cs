using PambolManager.API.Model.RequestModels;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class MatchRequestModelsExtensions
    {
        internal static Match ToMatch(this MatchRequestModel requestModel)
        {

            return new Match
            {
                RoundId = requestModel.RoundId,
                HomeTeamId = requestModel.HomeTeamId,
                AwayTeamId = requestModel.AwayTeamId,
                HomeGoals = requestModel.HomeGoals,
                AwayGoals = requestModel.AwayGoals,
                IsScoreSet = requestModel.IsScoreSet
            };
        }

        internal static Match ToMatch(this MatchUpdateRequestModel requestModel, Match existingMatch)
        {
            existingMatch.HomeGoals = requestModel.HomeGoals;
            existingMatch.AwayGoals = requestModel.AwayGoals;
            existingMatch.IsScoreSet = requestModel.IsScoreSet;

            return existingMatch;
        }
    }
}
