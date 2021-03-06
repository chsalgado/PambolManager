﻿using PambolManager.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.Domain.Entities
{
    public class Player : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // N:1 relationship with Team
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
