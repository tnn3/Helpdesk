﻿using System.Collections.Generic;
using Domain;

namespace WebApplication.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
        public ApplicationUser User { get; set; }
    }
}
