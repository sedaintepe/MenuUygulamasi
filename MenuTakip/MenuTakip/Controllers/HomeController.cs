using MenuTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using MenuTakip.Kullanici;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Net;
using MenuTakip.ViewData;


namespace MenuTakip.Controllers
{

    public class HomeController : Controller
    {
        //private UserManager<UserApps> userManager;
        //private RoleManager<AppRole> roleManager;
        VerimDbContext db = new VerimDbContext();
        string userIdm { get; set; }
        //
        // GET: /Home/
          public HomeController()
        {
            //UserStore<UserApps> userStore = new UserStore<UserApps>(db);
            //userManager = new UserManager<UserApps>(userStore);
            //userManager.UserValidator = new UserValidator<UserApps>(userManager) { AllowOnlyAlphanumericUserNames = false }; //Alphanumeric karakterler girilebilmesi sağlandı. 

            //RoleStore<AppRole> roleStore = new RoleStore<AppRole>(db);
            //roleManager = new RoleManager<AppRole>(roleStore);
        }
          #region methods

          [Authorize]
        public ActionResult Index()
        {
            HomeView model = new HomeView();
            model.Menuler = db.Menuler.ToList();
            model.Kisiler = db.Users.ToList();
            
           
         //Cart  shoppingcart=MaasViewModel
         
 //var cart = MaasViewModel.GetMaas(this.HttpContext);
 //// Set up our ViewModel
 //var viewModel = new IMaasViewModel
 //{
 //MaasItems = cart.GetMaasItems(),
 //MaasTotal = cart.GetTotal()
 //};

 //           return View(viewModel);
            return View(model);
        }

     public ActionResult bilgim()
     {
         IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
         string idm = authManager.User.Identity.GetUserId();
         var user = db.Users.Find(idm);
     
         return View(user);
     }
  [Authorize]

     public ActionResult Show()
     {
            
         IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
      
      
       //sen //var menuler = (from i in db.Menuler where (i.menuNo == 4 || i.menuNo==3||i.menuNo==5) select i).ToList();
         var menuler = db.Menuler.ToList();
       
         return View(menuler.ToList());
     }
//     
    
     public ActionResult Sec(string id)
     {
        MenuModel menumodel = db.Menuler
                .Include(p => p.Kisim)
                .Where(i => i.Kisi==id)
                .FirstOrDefault();
     
         //var menu = db.Menuler.Select(y => y.menuNo);
         //var c = db.Users.Select(s => s.Menu.menuNo);
         //if (ModelState.IsValid)
         //{
         //    c = menu;
           
         //}
         //db.SaveChanges();
         return View(menumodel);
     }
     public ActionResult price(int id)
     {
         IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
         string idm = authManager.User.Identity.GetUserId();
         var user = db.Users.Find(idm);
         user.maas = user.maas - id;
         UserApps home = new UserApps();
          home.maas=user.maas;
        
         db.SaveChanges();
        // ViewBag.yeni = home.maas;
         TempData["alertMessage"] = "Kalan Maaşınız=" +home.maas;
    
        
         return View();

     }


     //[HttpPost]
     //[ValidateAntiForgeryToken]

     //public ActionResult Sec(int? id)
     //{
     //    if (id == null)
     //    {
     //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
     //    }

     //    var menu = db.Menuler.Select(a => a.menuNo == id);

     //    // MenuModel menumodel = db.Menuler.Find(id);

     //    var systemUser = db.Users.Find(id);
     //    try
     //    {
     //        UserApps user = new UserApps();

     //        user.Menu.menuNo = id;



     //    }
     //    catch (Exception e)
     //    {
     //        throw e;
     //    }
      
      

     //    db.SaveChanges();
     //    ViewBag.Yeni = menu;
     //    return View(menu);
        //}




        #endregion methods
    }
}