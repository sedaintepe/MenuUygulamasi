using MenuTakip.Kullanici;
using MenuTakip.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace MenuTakip.Controllers
{
    public class AccountController : Controller
    {
       
       
        #region fields
        private UserManager<UserApps> userManager;
        private RoleManager<AppRole> roleManager;
        private VerimDbContext db = new VerimDbContext();

        #endregion fields

        #region ctor
        public AccountController()
        {
            UserStore<UserApps> userStore = new UserStore<UserApps>(db);
            userManager = new UserManager<UserApps>(userStore);
            userManager.UserValidator = new UserValidator<UserApps>(userManager) { AllowOnlyAlphanumericUserNames = false }; //Alphanumeric karakterler girilebilmesi sağlandı. 

            RoleStore<AppRole> roleStore = new RoleStore<AppRole>(db);
            roleManager = new RoleManager<AppRole>(roleStore);
        }

        #endregion ctor

        #region methods

        public ActionResult Login()
        {
            return View();
        }
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
              UserApps user = userManager.Find(model.UserName, model.Password);
               
                    if (user != null)
                    {
                        IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;

                        ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        AuthenticationProperties authProps = new AuthenticationProperties();
                        authProps.IsPersistent = model.RememberMe;
                        authManager.SignIn(authProps, identity);
                        return RedirectToAction("Index", "Home");
                       // return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("LoginUser", "Böyle bir kullanıcı bulunamadı");
                    }
               
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            
            model.AvailableRoles = GetUserRoles();
            model.Unvanlar = GetUnvanlar();
            return View(model);
        
        }
        [HttpPost]//doğrulama hatası
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

       public ActionResult Register(RegisterViewModel model)

      {
          model.AvailableRoles = GetUserRoles();
          model.Unvanlar = GetUnvanlar();
          
              IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
              string id = authManager.User.Identity.GetUserId();
            

              var systemUser = db.Users.Find(id);
              try
              {
                  UserApps user = new UserApps();

                  user.name = model.name;
                  user.soyad = model.soyad;
                  user.Email = model.Email;
                  user.UserName = model.UserName;
                  user.maas = model.maas;
                  user.tc = model.tc;
                  user.adres = model.adres;
                  user.cep = model.cep;
                  user.kartNo = model.kartNo;

                  IdentityResult iResult = userManager.Create(user, model.Password);
                  if (iResult.Succeeded)
                  {
                      // getRoleName methodundan seçilen rolün name i getiriyoruz.
                      userManager.AddToRole(user.Id, GetRoleName(model.RoleId)); //TODO 
                      userManager.GetRoles(user.Id).FirstOrDefault();

                      return RedirectToAction("List");
                  }
                  else
                  {
                      ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");
                  }
              }
              catch (Exception e)
              {
                  throw e;
              }
            
              return View(model);
         
    }
          
  
        [Authorize]
        public ActionResult ChangePassword(string id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            RegisterViewModel model = PrepareRegisterModel(user);
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            model.IsAdmin = authManager.User.IsInRole("Admin");
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(RegisterViewModel registerModel)
        {
            UserApps user = GetUser(registerModel.Id);
            userManager.RemovePassword(registerModel.Id);
            IdentityResult iResult = userManager.AddPassword(registerModel.Id, registerModel.Password);
            if (iResult.Succeeded)
            {
                return RedirectToAction("Edit", new { id = registerModel.Id });
            }
            else
            {
                ModelState.AddModelError("RegisterUser", "Şifre değiştirme işleminde hata!");
            }
            //}
            return View(registerModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            RegisterViewModel model = new RegisterViewModel();
            model = PrepareRegisterModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            string userId = authManager.User.Identity.GetUserId();
            var systemUser = db.Users.Find(userId);

            try
            {
                var user = db.Users.Find(id);
                //var role = user.Roles.FirstOrDefault().RoleId;
                //userManager.RemoveFromRole(user.Id, role.Use);
                db.Users.Remove(user);

                db.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return RedirectToAction("List");
        
        }
          


    
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            var model = new List<RegisterViewModel>();
          
            try
            {
                foreach (var item in db.Users.ToList())
                {
                    RegisterViewModel registerModel = PrepareRegisterModel(item);
                   
                    model.Add(registerModel);
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }
            return View(model.ToList());
        }
      
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
          
            var user = db.Users.Find(id);
            RegisterViewModel userModel = PrepareRegisterModel(user);

            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }
         [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var user = db.Users.Find(id);
            RegisterViewModel userModel = PrepareRegisterModel(user);
            userModel.AvailableRoles = GetUserRoles();
            userModel.Unvanlar = GetUnvanlar();
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            userModel.IsAdmin = authManager.User.IsInRole("Admin");
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(RegisterViewModel registerModel)
        {
          
                IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                string id = authManager.User.Identity.GetUserId();
                var systemUser = db.Users.Find(id);
             

                try
                {
                    UserApps user = GetUser(registerModel.Id);
                    user.name = registerModel.name;
                    user.Email = registerModel.Email;
                    user.soyad = registerModel.soyad;
                    user.UserName = registerModel.UserName;
                    user.maas = registerModel.maas;
                    user.tc = registerModel.tc;
                    user.adres=registerModel.adres;
                    user.cep=registerModel.cep;
                     user.kartNo=registerModel.kartNo;
                     registerModel.IsAdmin = authManager.User.IsInRole("Admin");
                     //if (user.Roles.FirstOrDefault().RoleId != registerModel.RoleId && registerModel.IsAdmin)//eğer kullanıcının rolü değişirse kullanıcı role tablosundan kullanıcının rolünü sil ve yeni rolü ekle
                     //{
                     //    var role = user.Roles.FirstOrDefault().Role;
                     //    userManager.RemoveFromRole(user.Id, role.Name);
                     //    userManager.AddToRole(user.Id, GetRoleName(registerModel.RoleId));
                     //}

                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = user.Id });
                }
                   
                catch (System.Exception e)
                {
                    if (e.Data != null)
                    {
                        throw e;
                    }
                    db.SaveChanges();
                }
                registerModel.AvailableRoles = GetUserRoles();
                registerModel.Unvanlar = GetUnvanlar();
            
            return RedirectToAction("Edit", new { id = registerModel.Id });
        }
        private UserApps GetUser(string id)
        {
            UserApps user = db.Users.Where(us => us.Id == id).FirstOrDefault();
            return user;
        }
        private List<SelectListItem> GetUserRoles()
        {
            return (from r in db.Roles.ToList()
                    select new SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Id.ToString()
                    }).ToList();
        }
       private string GetRoleName(string roleId)
        {
            string roleName = db.Roles.Where(r => r.Id == roleId).FirstOrDefault().Name;
            return roleName;
        }
       private float GetRoleName(float a)
       {
         
        
           return a;
       }
          [NonAction]
       private List<SelectListItem> GetUnvanlar()
       {
           return Enum.GetValues(typeof(isUnvan)).Cast<isUnvan>().Select(v => new SelectListItem
           {
               Text = v.ToString(),
               Value = ((int)v).ToString()
           }).ToList();
       }
        

        private RegisterViewModel PrepareRegisterModel(UserApps user)
        {
            RegisterViewModel userModel = new RegisterViewModel();
            userModel.name = user.name;
            userModel.Id = user.Id;
            userModel.Email = user.Email;
            userModel.soyad = user.soyad;
            userModel.UserName = user.UserName;
            userModel.maas = user.maas;
            userModel.tc = user.tc;
            userModel.adres = user.adres;
            userModel.cep = user.cep;
            userModel.kartNo = user.kartNo;
          



            return userModel;
        }
        #endregion methods
        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
 }   
}