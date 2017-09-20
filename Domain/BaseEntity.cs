using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class BaseEntity
    {
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Modified at")]
        public DateTime ModifiedAt { get; set; }

        public string CreatedById { get; set; }
        [Display(Name = "Created by")]
        public ApplicationUser CreatedBy { get; set; }

        public string ModifiedById { get; set; }
        [Display(Name = "Modified by")]
        public ApplicationUser ModifiedBy { get; set; }
    }
}
