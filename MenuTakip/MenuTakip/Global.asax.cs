using MenuTakip.Kullanici;
using MenuTakip.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MenuTakip
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            VerimDbContext db = new VerimDbContext();
            
                //Bu metod, eğer veritabanımız oluşturulmamış ise, oluşturulmasını sağlıyor.
                //db.Database.CreateIfNotExists();

                // Rol tanımlama 
                RoleStore<AppRole> roleStore = new RoleStore<AppRole>(db);
                RoleManager<AppRole> roleManager = new RoleManager<AppRole>(roleStore);
                if (!roleManager.RoleExists("Admin"))
                {
                    AppRole adminRole = new AppRole("Admin", "Sistem yöneticisi");
                    roleManager.Create(adminRole);
                }


                if (!roleManager.RoleExists("User"))
                {
                    AppRole userRole = new AppRole("User", "Sistem kullanıcısı");
                    roleManager.Create(userRole);
                }

            }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
        }
    
}
