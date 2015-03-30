using PambolManager.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Round : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public int RoundNumber { get; set; }
    }
}