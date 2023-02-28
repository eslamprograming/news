using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DEmo.Models;

namespace DEmo.Controllers
{
    public class comment_newsController : Controller
    {
        
        private blogdatabase db = new blogdatabase();

        // GET: comment_news
        public ActionResult Index(int id)
        {
            List<comment_news> cn = db.comment_news.Where(n => n.news_id == id).ToList();
            ViewBag.id = id;

            return View(cn);
        }

        // GET: comment_news/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var comment_news = db.comment_news.Find(id);
            //if (comment_news == null)
            //{
            //    return HttpNotFound();
            //}
            return View(comment_news);
        }

        // GET: comment_news/Create
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            //ViewBag.news_id = new SelectList(db.news, "id", "title");
            //ViewBag.id_user = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: comment_news/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(comment_news comment_news)
        {
            if (Session["id"] == null) return RedirectToAction("Home", "login");
            comment_news cns = new comment_news();
            cns.news_id = comment_news.news_id;
            
            cns.id_user = (int)Session["id"];
            cns.comments = comment_news.comments;
            int id = comment_news.news_id;
            db.comment_news.Add(cns);
            db.SaveChanges();
            return RedirectToAction("index", new {id= comment_news.news_id }); 
        }

        

        // GET: comment_news/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment_news comment_news = db.comment_news.Find(id);
            if (comment_news == null)
            {
                return HttpNotFound();
            }
            ViewBag.news_id = new SelectList(db.news, "id", "title", comment_news.news_id);
            ViewBag.id_user = new SelectList(db.users, "id", "username", comment_news.id_user);
            return View(comment_news);
        }

        // POST: comment_news/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "news_id,id_user,comments")] comment_news comment_news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment_news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.news_id = new SelectList(db.news, "id", "title", comment_news.news_id);
            ViewBag.id_user = new SelectList(db.users, "id", "username", comment_news.id_user);
            return View(comment_news);
        }

        // GET: comment_news/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment_news comment_news = db.comment_news.Find(id);
            if (comment_news == null)
            {
                return HttpNotFound();
            }
            return View(comment_news);
        }

        // POST: comment_news/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment_news comment_news = db.comment_news.Find(id);
            db.comment_news.Remove(comment_news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
