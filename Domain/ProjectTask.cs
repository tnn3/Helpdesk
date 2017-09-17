using System.Collections.Generic;

namespace Domain
{
    public class ProjectTask : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double ComponentPrice { get; set; }
        public bool PaidWork { get; set; }
        public int AmountDone { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }

        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public List<ChangeSet> ChangeSets { get; set; }
        public List<CustomFieldInTasks> CustomFields { get; set; }
        public List<CustomFieldValue> CustomFieldValues { get; set; }
    }
}
