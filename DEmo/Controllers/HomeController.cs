using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DEmo.Models;

namespace DEmo.Controllers
{
    public class HomeController : Controller
    {
        blogdatabase db = new blogdatabase();
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult login()
        {
            HttpCookie reqCookies = Request.Cookies["file"];
            if (reqCookies != null)
            {
                Session["id"] = int.Parse(reqCookies["id"]);
                return RedirectToAction("profile", "user");
            }
            return View();
        }
        [HttpPost]
        public ActionResult login(string username, string password,string check)
        {
            user u = db.users.Where(n => n.username == username && n.password == password).FirstOrDefault();
            if (u != null)
            {
                Session["id"] = u.id;
                if(check=="true")
                {
                    HttpCookie co = new HttpCookie("file");
                    //co.Value.Prepend("id", u.id);
                    co["id"] = u.id.ToString();
                    co.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(co);
                }
                return RedirectToAction("profile", "user");
            }
            TempData["Error"] = "User name or Password is Error";
            return View();
            
        }
        public ActionResult logout()
        {
            Session["id"] = null;
            HttpCookie co = new HttpCookie("file");
            co.Expires = DateTime.Now.AddDays(-15);
            Response.Cookies.Add(co);
            return View("Index");
        }
        public ActionResult blogs()
        {
            return View(db.news.ToList());
        }
        public ActionResult descrip(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            List<like_news> lkn= db.like_news.Where(n => n.news_id == id).ToList();
            List<comment_news> commentN = db.comment_news.Where(n => n.news_id == id).ToList();
            int idsession = (int)Session["id"];
            ViewBag.value = "false";
            foreach (var item in lkn)
            {
                if (item.news_id == id)
                {
                    if (item.id_user == idsession)
                    {
                        ViewBag.value = "true";
                    }
                }



            }
            TempData["count"] = lkn.Count;
            TempData["comment"] = commentN.Count();
            
            return View(db.news.Find(id));
        }
        public ActionResult userprofile(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");

            List<like> like = db.likes.Where(n => n.user_id == id).ToList();
            ViewBag.likecount = like.Count();
            int idsession = (int)Session["id"];
            ViewBag.value = "false";
            foreach (var item in like)
            {
                if (item.user_id == id)
                {
                    if (item.id_user == idsession)
                    {
                        ViewBag.value = "true";
                    }
                }
                
                

            }

            List<comment> comment = db.comments.Where(n => n.user_id == id).ToList();
            ViewBag.commentcount = comment.Count();

            user userprofile = db.users.Where(n => n.id == id).FirstOrDefault();
            return View(userprofile);
        }
        public ActionResult like(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login");
            List<like> like = db.likes.Where(n => n.user_id == id).ToList();
            int other = (int)Session["id"];
            like lk = new like();
            lk.user_id = id;
            lk.id_user = other;
            lk.like1 = "like";
            foreach (var item in like)
            {
                if (id ==item.user_id)
                {
                    if (item.id_user == other)
                    {
                        db.likes.Remove(item);
                        db.SaveChanges();
                        return RedirectToAction("userprofile", new { id = lk.user_id });
                    }
                }
            }
            db.likes.Add(lk);
            db.SaveChanges();
            
            return RedirectToAction("userprofile", new { id =lk.user_id});
        }
        public ActionResult comment(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login");

            ViewBag.id = id;
            List<comment> com = db.comments.Where(n => n.user_id == id).ToList();
            return View(com);
        }
        public ActionResult Create(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login");
            ViewBag.id = id;

            return View();
        }
        [HttpPost]
        public ActionResult Create(comment c)
        {
            if (Session["id"] == null) return RedirectToAction("login");

            comment c2 = new comment();
            c2.comment1 = c.comment1;
            c2.user_id = c.user_id;
            c2.id_user = (int)Session["id"];
            int id = c2.user_id;
            db.comments.Add(c2);
            db.SaveChanges();
            //return RedirectToAction("blogs");
            return RedirectToAction("comment", new {id=id});
        }
    }
}