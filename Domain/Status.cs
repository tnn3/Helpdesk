using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Status")]
        public string Name { get; set; }
    }
}
