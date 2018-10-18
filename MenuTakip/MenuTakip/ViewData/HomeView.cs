using MenuTakip.Kullanici;
using MenuTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuTakip.ViewData
{
    public class HomeView
    {
        public List<UserApps> Kisiler { get; set; }
        public List<MenuModel> Menuler { get;set;}
    }
}