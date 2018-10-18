using MenuTakip.Kullanici;
using MenuTakip.Models;
using MenuTakip.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MenuTakip.Controllers
{
    public class MenuYeniController : Controller
    {
        #region fields
        private VerimDbContext db = new VerimDbContext();
        #endregion fields
        #region methods
        [Authorize(Roles = "Admin")]
        // GET: /MenuYeni/
        public ActionResult Index()
        {
          
          //  var menuler = from i in db.Menuler where (i.Yemek.YemekKategoriId == 2) select i;
            var menuler = db.Menuler.Include(m => m.Yemek);
            
            return View(menuler.ToList());
        }
        [Authorize(Roles = "Admin")]

        // GET: /MenuYeni/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuModel menumodel = db.Menuler.Find(id);
            if (menumodel == null)
            {
                return HttpNotFound();
            }
            return View(menumodel);
        }

        // GET: /MenuYeni/Create
      
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {

          
            var menu= new MenuModel();
            menu.Yemeks = new List<YemekModel>();
            YemekEkleData(menu);
           

         
         
            return View();
        }

        // POST: /MenuYeni/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(MenuModel menumodel, string[] selectedYemeks,string[] selectedKisiler)
        {

            if (selectedYemeks != null)
            {
                menumodel.Yemeks = new List<YemekModel>();
                foreach (var ymk in selectedYemeks)
                {
                    var yemekToAdd = db.Yemekler.Find(int.Parse(ymk));
                    menumodel.Yemeks.Add(yemekToAdd);
                    
                }
            }
            if (ModelState.IsValid)
            {
               

                    db.Menuler.Add(menumodel);
                  
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            YemekEkleData(menumodel);
              
            return View(menumodel);
        }

    [Authorize]

        // GET: /MenuYeni/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // MenuModel menumodel = db.Menuler.Find(id);
            MenuModel menumodel = db.Menuler
                .Include(p => p.Yemeks)
                .Where(i => i.menuId == id)
                .Single();
           
            if (menumodel == null)
            {
                return HttpNotFound();
            }
      //   ViewBag.yemekId = new SelectList(db.Yemekler, "yemekId", "yemekAd", menumodel.menuId);
            YemekEkleData(menumodel);
            return View(menumodel);
        }
        private void YemekEkleData(MenuModel menu)
        {
            var allYemeks = db.Yemekler;
            var SeciliYemeks = new HashSet<int>(menu.Yemeks.Select(b => b.yemekId));
            var viewModel = new List<menusYemekVM>();
            foreach (var yemek in allYemeks)
            {
                viewModel.Add(new menusYemekVM
                {
                    yemekId = yemek.yemekId,
                    yemeAd = yemek.yemekAd,
                    Assigned = SeciliYemeks.Contains(yemek.yemekId)
                });
            }
            ViewBag.Yemekler = viewModel;
        }
    

        // POST: /MenuYeni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int? id, string[] selectedYemeks){
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuToUpdate = db.Menuler
                .Include(p => p.Yemeks)
                .Where(i => i.menuId == id)
                .Single();
            if (TryUpdateModel(menuToUpdate, "",
                   new string[] { "menuId", "Kisi", "tarih", "menuFiyat" }))
            {
                try
                {
                    UpdateMenusYemeks(selectedYemeks, menuToUpdate);

                    db.Entry(menuToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            YemekEkleData(menuToUpdate);
            return View(menuToUpdate);
        }
       

       
        private void UpdateMenusYemeks(string[] selectedYemeks, MenuModel menuToUpdate)
        {
            if (selectedYemeks == null)
            {
                menuToUpdate.Yemeks = new List<YemekModel>();
                return;
            }

            var selectedYemeksHS = new HashSet<string>(selectedYemeks);
            var menuYemeks = new HashSet<int>
                (menuToUpdate.Yemeks.Select(b => b.yemekId));
            foreach (var yemek in db.Yemekler)
            {
                if (selectedYemeksHS.Contains(yemek.yemekId.ToString()))
                {
                    if (!menuYemeks.Contains(yemek.yemekId))
                    {
                        menuToUpdate.Yemeks.Add(yemek);
                    }
                }
                else
                {
                    if (menuYemeks.Contains(yemek.yemekId))
                    {
                       menuToUpdate.Yemeks.Remove(yemek);
                    }
                }
            }
        }
    [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuModel menumodel = db.Menuler.Find(id);
           
            if (menumodel == null)
            {
                return HttpNotFound();
            }
            return View(menumodel);
        }

        // POST: /MenuYeni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var bir = db.Menuler.Find(id);
            var iki = db.Yemekler.Find(id);
            db.Entry(bir).Collection("Yemeks");
        
            bir.Yemeks.Remove(iki);
         MenuModel menumodel = db.Menuler.Find(id);
        
        db.Menuler.Remove(menumodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public List<SelectListItem> GetKategoriler()
        {
            return Enum.GetValues(typeof(YemekKategoriModel)).Cast<YemekKategoriModel>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
        }
        #endregion methods

       
       
    }
}
