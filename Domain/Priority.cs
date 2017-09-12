using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Priority
    {
        public int PriorityId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
