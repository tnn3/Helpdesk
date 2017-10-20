﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TaskUser : BaseEntity
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
