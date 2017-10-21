using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Change
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Before { get; set; }
        [MaxLength(200)]
        public string After { get; set; }

        public string Fieldname { get; set; }

        public int ChangeSetId { get; set; }
        public ChangeSet ChangeSet { get; set; }
    }
}
