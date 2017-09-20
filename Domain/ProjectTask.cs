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
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Display(Name = "Component price")]
        public double ComponentPrice { get; set; }
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

        public string AssignedToId { get; set; }
        [Display(Name = "Assigned to")]
        public ApplicationUser AssignedTo { get; set; }
        [Required]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public virtual List<ChangeSet> ChangeSets { get; set; }
        public virtual List<CustomFieldInTasks> CustomFields { get; set; }
        public virtual List<CustomFieldValue> CustomFieldValues { get; set; }
    }
}
