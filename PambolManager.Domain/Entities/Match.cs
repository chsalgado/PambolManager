using PambolManager.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Match : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        // N:2 relationship with Team
        // Special management for this relationship in EntitiesContext.OnModelCreation()
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }

        // Score
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public bool IsScoreSet { get; set; }

        // N:1 relationship with Round
        public Guid RoundId { get; set; }
        public virtual Round Round { get; set; }
    }
}
