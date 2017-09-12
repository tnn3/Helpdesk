using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class CustomFieldValue
    {
        public int CustomFieldValueId { get; set; }
        public string FieldValue { get; set; }

        public int CustomFieldId { get; set; }
        public CustomField CustomField { get; set; }

        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
