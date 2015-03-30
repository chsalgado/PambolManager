using PambolManager.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Tournament : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public string TournamentName { get; set; }

        [Required]
        public int TotalRounds { get; set; }

        [Required]
        public int MaxTeams { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}