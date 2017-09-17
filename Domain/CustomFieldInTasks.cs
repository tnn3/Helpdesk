using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class CustomFieldInTasks : BaseEntity
    {
        public int Id { get; set; }

        public int CustomFieldId { get; set; }
        public CustomField CustomField { get; set; }
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }

        public List<CustomFieldValue> CustomFieldValues { get; set; }
    }
}
