using MenuTakip.Kullanici;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MenuTakip.Models
{
    public class KisiMenuModel
    {
        [Key]

        public int kisiMenuId { get; set; }
        // [ForeignKey("yemekIdm")]
        public int kisiIdm { get; set; }
        // [ForeignKey("menuIdm")]
        public int menuIdm { get; set; }
        public MenuModel Menu { get; set; }
        public UserApps Kisi { get; set; }

    }
}