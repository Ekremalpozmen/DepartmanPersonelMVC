using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModel;

namespace PersonelMVCUI.Controllers
{

    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (departman.Ad == null)
            {
                return View("DepartmanForm");
            }
            MesajViewModel model = new MesajViewModel();
            if (departman.Id == 0)
            {
                db.Departman.Add(departman);
                model.Mesaj = departman.Ad + "başarıyla eklendi";
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    guncellenecekDepartman.Ad = departman.Ad;
                    model.Mesaj = departman.Ad + "başarıyla güncellendi";

                }

            }
            db.SaveChanges();

            model.Status = true;
            model.LinkText = "Departman Listesi";
            model.Url = "/Departman";
            return View("_Mesaj", model);

        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("DepartmanForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Departman.Remove(silinecekDepartman);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }


}