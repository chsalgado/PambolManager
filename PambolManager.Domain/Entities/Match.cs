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

        // 1:1 relationship with Score
        public Guid ScoreId { get; set; }
        public virtual Score Score { get; set; }
    }
}
