using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MenuTakip.Models
{
    public class YemekMenuModel
    {
        [Key]
       
        public int yemekMenuId { get; set; }
       // [ForeignKey("yemekIdm")]
        public int yemekIdm { get; set; }
        // [ForeignKey("menuIdm")]
        public int menuIdm { get; set; }
        public  YemekModel Yemek { get; set; }
        public  MenuModel Menu { get; set; }


    }
}