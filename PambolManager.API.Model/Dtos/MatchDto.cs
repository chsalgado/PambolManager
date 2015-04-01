using System;

namespace PambolManager.API.Model.Dtos
{
    public class MatchDto : IDto
    {
        public Guid Id { get; set; }
        
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public ScoreDto Score { get; set; }
    }
}
