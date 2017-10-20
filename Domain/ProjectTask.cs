using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProjectTask : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Component price")]
        public decimal ComponentPrice { get; set; }
        [Display(Name = "Paid work")]
        public bool PaidWork { get; set; }
        [Range(0, 100)]
        [Display(Name = "Amount done")]
        public int AmountDone { get; set; }
        [Display(Name = "Client name")]
        [MaxLength(100)]
        public string ClientName { get; set; }
        [Required]
        [Display(Name = "Client phone")]
        public string ClientPhone { get; set; }
        [Display(Name = "Client email")]
        public string ClientEmail { get; set; }
        public string AssigneeId { get; set; }
        public ApplicationUser Assignee { get; set; }

        [Required]
        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
        [Required]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual List<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();
        public virtual List<ChangeSet> ChangeSets { get; set; } = new List<ChangeSet>();
        public virtual List<CustomFieldInTasks> CustomFields { get; set; } = new List<CustomFieldInTasks>();
        public virtual List<CustomFieldValue> CustomFieldValues { get; set; } = new List<CustomFieldValue>();
    }
}
