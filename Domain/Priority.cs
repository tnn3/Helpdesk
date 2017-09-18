using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Priority
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Priority")]
        public string Name { get; set; }

        public virtual List<ProjectTask> ProjectTasks { get; set; }
    }
}
