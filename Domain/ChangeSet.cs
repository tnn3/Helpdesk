using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class ChangeSet : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Comment { get; set; }

        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }

        public virtual List<Change> Changes { get; set; }
    }
}
