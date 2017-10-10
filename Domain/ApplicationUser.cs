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
        [Required]
        [Display(Name = "First name")]
        [MaxLength(50)]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Last name")]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Display(Name = "Created at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Modified at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime ModifiedAt { get; set; }

        public int? TitleId { get; set; }
        [Display(Name = "Title")]
        public UserTitle Title { get; set; }

        [Display(Name = "Name")]
        public string FirstLastname => Firstname + " " + Lastname;
    }
}
