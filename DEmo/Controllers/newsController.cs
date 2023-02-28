using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DEmo.Models;

namespace DEmo.Controllers
{
    public class newsController : Controller
    {
        blogdatabase db = new blogdatabase();
        public ActionResult usernews()
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            int id = (int)Session["id"];

            var mynews = db.news.Where(n => n.user.id == id && n.delete==1).ToList();
            return View(mynews);
        }
        public ActionResult add()
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            List<catalog> cat = db.catalogs.ToList();
            SelectList st = new SelectList(cat, "id", "name");
            ViewBag.cat = st;
            return View();
        }
        [HttpPost]
        public ActionResult add(news n,HttpPostedFileBase img)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            int id = (int)Session["id"];
            var nn = db.users.Find(id);
            img.SaveAs(Server.MapPath("/newsphoto" + img.FileName));
            n.photo = "/newsphoto" + img.FileName;
            n.date = DateTime.Now;
            n.user_id = id;
            n.state = "ok";
            n.admin_id = 1;
            n.cat_id = nn.catalog_id;
            n.delete = 1;
            if (ModelState.IsValid)
            {
                db.news.Add(n);
                db.SaveChanges();
                return RedirectToAction("usernews");
            }
            return View();
        }
        public ActionResult Detalis(int id)
        {

            if (Session["id"] == null) return RedirectToAction("login", "Home");

            return View(db.news.Find(id));
        }
        public ActionResult Edit(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            SelectList cat_id1 = new SelectList(db.catalogs.ToList(), "id","name");
            ViewBag.cat_id = cat_id1;

            SelectList user_id1 = new SelectList(db.users.ToList(), "id", "username");
            ViewBag.user_id = user_id1;

            SelectList admin_id1 = new SelectList(db.admins.ToList(), "admin_id", "username");
            ViewBag.admin_id = admin_id1;

            return View(db.news.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(int id,news n)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            var old = db.news.Find(id);
            old.id = n.id;
            old.title = n.title;
            old.catalog = n.catalog;
            old.date = n.date;
            old.desc = n.desc;
            old.bref = n.bref;
            //old.photo = n.photo;
            old.state = n.state;
            old.user = n.user;
            old.admin = n.admin;
            old.admin_id = n.admin_id;
            old.cat_id = n.cat_id;
            old.delete = 1;
            old.comment_news = n.comment_news;
            old.like_news = n.like_news;
            old.user_id = (int)Session["id"];
            db.SaveChanges();
            return RedirectToAction("usernews");
        }
        public ActionResult Delete(int? id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            return View(db.news.Find(id));
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            var news =db.news.Where(n => n.id == id).SingleOrDefault();
            news.delete = 0;
            db.SaveChanges();
            return RedirectToAction("usernews");
        }
        public ActionResult like(int id)
        {
            List<like_news> lk = db.like_news.Where(n => n.news_id == id).ToList();
            ViewBag.like = lk.Count;

            return View();
        }
    }
}