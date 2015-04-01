using System;

namespace PambolManager.API.Model.Dtos
{
    public class ScoreDto : IDto
    {
        public Guid Id { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }
    }
}
