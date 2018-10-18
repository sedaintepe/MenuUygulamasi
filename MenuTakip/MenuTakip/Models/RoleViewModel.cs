using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MenuTakip.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    }
}