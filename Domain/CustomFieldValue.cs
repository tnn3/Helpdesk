using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class CustomFieldValue
    {
        public int Id { get; set; }
        public string FieldValue { get; set; }

        public int CustomFieldInTasksId { get; set; }
        public CustomFieldInTasks CustomField { get; set; }
    }
}
