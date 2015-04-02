using PambolManager.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Tournament : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TournamentName { get; set; }

        [Required]
        public int TotalRounds { get; set; }

        [Required]
        public int MaxTeams { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        // N:1 relationship with FieldManager
        public string FieldManagerId { get; set; }
        public virtual FieldManager FieldManager { get; set; }

        // 1:N relationship with Teams
        public virtual ICollection<Team> Teams { get; set; }

        // 1:N relationship with Rounds
        public virtual ICollection<Round> Rounds { get; set; }
    }
}