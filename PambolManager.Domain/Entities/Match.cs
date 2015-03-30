using PambolManager.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Match : IEntity
    {
        [Key]
        public Guid Key { get; set; }
    }
}
