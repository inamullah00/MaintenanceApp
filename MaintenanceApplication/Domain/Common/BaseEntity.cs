﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for all entities
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Default value when created
        public DateTime? UpdatedAt { get; set; } // Nullable - updated when modified
    }
}
