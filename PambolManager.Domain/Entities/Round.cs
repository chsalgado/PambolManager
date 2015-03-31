using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Round : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int RoundNumber { get; set; }

        // N:1 relationship with Tournament
        public Guid TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        // 1:N relationship with Match
        public virtual ICollection<Match> Matches { get; set; }
    }
}