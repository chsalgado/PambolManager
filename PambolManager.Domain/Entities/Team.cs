using PambolManager.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Team : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}
