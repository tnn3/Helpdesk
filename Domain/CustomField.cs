using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Domain.Enums;

namespace Domain
{
    public class CustomField
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FieldName { get; set; }
        public FieldType FieldType { get; set; }
        [MaxLength(50)]
        public string PossibleValues { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }

        public List<CustomFieldValue> CustomFieldValues { get; set; }
        public List<CustomFieldInTasks> Tasks { get; set; }
    }
}
