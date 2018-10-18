using MenuTakip.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MenuTakip.Controllers
{
    public class YemekYeniController : Controller
    {
        
        private VerimDbContext db = new VerimDbContext();

        // GET: /YemekYeni/
        public async Task<ActionResult> Index()
        {
            return View(await db.Yemekler.ToListAsync());
        }
        //public ActionResult Index()
        //{
        //    return View(db.Yemekler.ToList());
        //}

        // GET: /YemekYeni/Details/5

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YemekModel yemekmodel = await db.Yemekler.FindAsync(id);
           
            if (yemekmodel == null)
            {
                return HttpNotFound();
            }
            return View(yemekmodel);
        }

        // GET: /YemekYeni/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            YemekModel model = new YemekModel();
          model.YemekKategoris = GetKategoriler();
            return View(model);
        }

        // POST: /YemekYeni/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create( YemekModel yemekmodel)
        //[Bind(Include="yemekId,yemekAd,YemekKategoriModel")]
        {

            //yemekmodel.YemekKategoris = GetKategoriler();
            if (ModelState.IsValid)
            {
                db.Yemekler.Add(yemekmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yemekmodel);
        }

        // GET: /YemekYeni/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YemekModel yemekmodel = db.Yemekler.Find(id);
            if (yemekmodel == null)
            {
                return HttpNotFound();
            }
            return View(yemekmodel);
        }

        // POST: /YemekYeni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="yemekId,yemekAd")] YemekModel yemekmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yemekmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yemekmodel);
        }

        [Authorize(Roles = "Admin")]

        // GET: /YemekYeni/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YemekModel yemekmodel = db.Yemekler.Find(id);
            if (yemekmodel == null)
            {
                return HttpNotFound();
            }
            return View(yemekmodel);
        }

        // POST: /YemekYeni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            YemekModel yemekmodel = db.Yemekler.Find(id);
            db.Yemekler.Remove(yemekmodel);
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
          [NonAction]
        public List<SelectListItem> GetKategoriler()
        {
            return Enum.GetValues(typeof(YemekKategoriModel)).Cast<YemekKategoriModel>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
        }
    }
}
