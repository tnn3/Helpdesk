﻿using Microsoft.AspNetCore.Identity;

namespace Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int TitleId { get; set; }
        public UserTitle Title { get; set; }
    }
}
