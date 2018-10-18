using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MenuTakip.Models
{
    public class MenuViewModel
    {
        [Key]
         public int? ID { get; set; } // included so this can be used for editing as well as creating
       public string isim { get; set; }
    
    public List<YemekModel> yemekler { get; set; }

    }
}