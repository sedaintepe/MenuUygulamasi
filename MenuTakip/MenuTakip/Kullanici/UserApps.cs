using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MenuTakip.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace MenuTakip.Kullanici
{
   
     public class UserApps: IdentityUser
    {
         public string name { get; set; }
         public string soyad { get; set; }
         public int maas { get; set; }
         public virtual ICollection<MenuModel> Menuler { get; set; }
         public int idyeni { get; set; }
         public MenuModel Menu { get; set; }
         public string kartNo { get; set; }
         public string cep { get; set; }
         public string adres { get; set; }
         public string tc { get; set; }
  
        
       //iş unvan
    }
}
