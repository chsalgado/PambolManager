using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PambolManager.Models
{
    public class Team : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}