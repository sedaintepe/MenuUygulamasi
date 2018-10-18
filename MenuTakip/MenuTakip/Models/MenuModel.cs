using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MenuTakip.Kullanici;

namespace MenuTakip.Models
{
    public class MenuModel
    {
        [Key]
        public int menuId { get; set;}
       
        //[ForeignKey("YemekModel")]
       // [ForeignKey("Yemek")]
       public int yemekId{get;set;}
        public virtual YemekModel Yemek { get; set; }
        [Display(Name = "SiparişVeren")]
       
        public string Kisi { get; set; }
        public virtual UserApps Kisim{ get; set; }
        public string menuBaslik{ get; set; }
        public int? menuNo { get; set; }
         [Display(Name = "Tarih")]
        public DateTime tarih { get; set; }
         [Display(Name = "Fiyat")]

         //[DataType(DataType.Currency)]
         //public float? menuFiyat { get; set; }
         public int menuFiyat { get; set; }
           public IEnumerable<SelectListItem> YemekKategoris { get; set; }
           public int YemekKategoriId { get; set; }

           public YemekKategoriModel YemekKategoriModel
           {
               get
               {
                   return (YemekKategoriModel)this.YemekKategoriId;
               }
               set
               {
                   this.YemekKategoriId = (int)value;
               }
           }
           public virtual ICollection<YemekModel>Yemeks { get; set; }
           public virtual ICollection<UserApps> Kullanicilar { get; set; }
         
   //public List<string> SelectedYemekList { get; set; }
        
  
    
   
    
    }
}