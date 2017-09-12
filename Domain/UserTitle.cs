using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class UserTitle
    {
        public int UserTitleId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
    }
}
