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
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }

        // 1:1 relationship with Score
        public Guid ScoreId { get; set; }
        public virtual Score Score { get; set; }
    }
}
