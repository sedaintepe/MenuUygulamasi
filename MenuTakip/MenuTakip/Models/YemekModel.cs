using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MenuTakip.Models
{
    public class YemekModel
    {
        [Key]
        public int yemekId { get; set; }
        public string yemekAd { get; set; }
      // public virtual List<YemekViewModel> yemekSec { get; set; }
        public int YemekKategoriId { get; set; }
       public IEnumerable<SelectListItem> YemekKategoris { get; set; }
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
      public virtual ICollection<MenuModel> Menuler { get; set; }

    }
}