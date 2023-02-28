using DEmo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DEmo.Controllers
{
    public class like_newsController : Controller
    {
        private blogdatabase db = new blogdatabase();


        public ActionResult Index(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            List<like_news> lknews = db.like_news.Where(n => n.news_id == id).ToList();

            return View(lknews);
        }
        public ActionResult Create(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");


            like_news lkn = new like_news();
            lkn.news_id = id;
            int id1 = id;
            lkn.id_user = (int)Session["id"];
            lkn.like = 1;
            List<like_news> lknews = db.like_news.Where(n => n.news_id == id).ToList();

            foreach (var item in lknews)
            {
                if (item.id_user == lkn.id_user)
                {
                    if (item.news_id == lkn.news_id)
                    {
                        db.like_news.Remove(item);
                        db.SaveChanges();
                        return RedirectToAction("descrip","Home",new { id =id });
                    }
                }

            }
            db.like_news.Add(lkn);
            db.SaveChanges();
            return RedirectToAction("descrip","Home",new { id=id});
        }
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            like_news like_news = db.like_news.Find(id);
            db.like_news.Remove(like_news);
            db.SaveChanges();
            return RedirectToAction("blogs", "Home");
        }

    }
}