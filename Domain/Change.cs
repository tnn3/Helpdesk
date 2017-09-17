using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Change
    {
        public int Id { get; set; }
        public string Before { get; set; }
        public string After { get; set; }

        public int ChangeSetId { get; set; }
        public ChangeSet ChangeSet { get; set; }
    }
}
