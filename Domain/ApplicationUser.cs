using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /*[Display(Name = "First name")]
        public string Firstname { get; set; }
        [Display(Name = "Last name")]
        public string Lastname { get; set; }*/

        [Display(Name = "Created at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Modified at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime ModifiedAt { get; set; }

        public int? TitleId { get; set; }
        [Display(Name = "Title")]
        public UserTitle Title { get; set; }

        [Display(Name = "Disabled")]
        public bool IsDisabled { get; set; }

        //public virtual ICollection<IdentityUserRole<int>> Roles { get; } = new List<IdentityUserRole<int>>();
    }
}
