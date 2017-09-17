using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.ViewModels
{
    public class ProjectTaskCreateEditViewModel
    {
        public ProjectTask ProjectTask { get; set; }
        public SelectList Statuses { get; set; }
        public SelectList Priorities { get; set; }
    }
}
