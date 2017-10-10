using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Areas.Admin.ViewModels
{
    public class UserCreateEditViewModel
    {
        public ApplicationUser User { get; set; }
        [Display (Name = "Role")]
        public string RoleId { get; set; }
        public SelectList TitleSelectList { get; set; }
        public SelectList RoleSelectList { get; set; }
    }
}
