using System;

namespace PambolManager.API.Model.Dtos
{
    public class MatchDto : IDto
    {
        public Guid Id { get; set; }
        
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }

        // Score
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public bool IsScoreSet { get; set; }

        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
    }
}
