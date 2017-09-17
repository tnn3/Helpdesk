using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class ChangeSet : BaseEntity
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }

        public int ChangerId { get; set; }
        public ApplicationUser Changer { get; set; }

        public List<Change> Changes { get; set; }
    }
}
