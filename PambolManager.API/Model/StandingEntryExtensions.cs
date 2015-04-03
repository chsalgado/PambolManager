using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class StandingEntryExtensions
    {
        internal static StandingEntryDto ToStandingEntryDto(this StandingEntry standingEntry)
        {

            return new StandingEntryDto
            {
                TeamName = standingEntry.TeamName,
                PlayedMatches = standingEntry.PlayedMatches,
                WonMatches = standingEntry.WonMatches,
                DrawedMatches = standingEntry.DrawedMatches,
                LostMatches  = standingEntry.LostMatches,
                ScoredGoals = standingEntry.ScoredGoals,
                ReceivedGoals = standingEntry.ReceivedGoals,
                GoalsDifference = standingEntry.GoalsDifference,
                Points = standingEntry.Points
            };
        }
    }
}
