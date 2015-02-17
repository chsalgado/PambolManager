using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PambolManager.Models
{
    public class Match : IEntity
    {
        [Key]
        public Guid Key { get; set; }
    }
}