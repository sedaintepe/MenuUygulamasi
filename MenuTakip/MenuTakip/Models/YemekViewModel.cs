using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuTakip.Models
{
    public class YemekViewModel
    {
        public int Id { get; set; }
        public string isim { get; set; }
        public bool Checked { get; set; }
    }
}