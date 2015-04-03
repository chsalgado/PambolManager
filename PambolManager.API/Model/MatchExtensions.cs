using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class MatchExtensions
    {
        internal static MatchDto ToMatchDto(this Match match)
        {
            return new MatchDto
            {
                Id = match.Id,
                HomeTeam = match.HomeTeam.ToTeamDto(),
                AwayTeam = match.AwayTeam.ToTeamDto(),
                HomeGoals = match.HomeGoals,
                AwayGoals = match.AwayGoals,
                IsScoreSet = match.IsScoreSet,
            };
        }
    }
}
