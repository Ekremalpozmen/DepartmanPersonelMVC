using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntityFramework;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        PersonelDbEntities db = new PersonelDbEntities();

        public ActionResult Index()
        {
            var model = db.Personel.Include(x => x.Departman).ToList();
            return View(model);
        }

        [Authorize(Roles = "A")]
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personel()
            };
            return View("PersonelForm", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            if (personel.Id == 0)//ekleme işlemi
            {
                db.Personel.Add(personel);
            }
            else //güncelleme işlemi
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;

            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)
            };
            return View("PersonelForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel == null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x => x.DepartmanId == id).ToList();
            return PartialView(model);
        }

        public int? ToplamMaas()
        {
            return db.Personel.Sum(x => x.Maas);
        }
    }
}