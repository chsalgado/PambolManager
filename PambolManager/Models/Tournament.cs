using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PambolManager.Models
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