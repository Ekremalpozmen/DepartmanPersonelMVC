﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PersonelMVCUI.Models.EntityFramework;

namespace PersonelMVCUI.Controllers
{


    public class SecurityController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Security
        [AllowAnonymous]

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                return RedirectToAction("Index", "Departman");
            }
            else
            {
                ViewBag.Mesaj = "geçersiz kullanıcı adı şifre";
                return View();

            }


        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}