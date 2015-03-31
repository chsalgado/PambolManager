using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Team : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TeamName { get; set; }

        public string LogoPath { get; set; }

        // N:1 relationship with Tournament
        public Guid TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        // 1:N relationship with Player
        public virtual ICollection<Player> Players { get; set; }

        // 2:N relationship with Match (need two collections since one can´t be referenced by two foreign keys)
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}
