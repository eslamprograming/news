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
    public class userController : Controller
    {
        blogdatabase db = new blogdatabase();

        
        public ActionResult profile()
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");
           
            int id = (int)Session["id"];


            List<like> like = db.likes.Where(n => n.user_id == id).ToList();
            ViewBag.likecount = like.Count();
            ViewBag.like = like;
            List<comment> comment = db.comments.Where(n => n.user_id == id).ToList();
            ViewBag.comment = comment;
            ViewBag.commentcount = comment.Count();
           

            user u = db.users.Where(n => n.id == id).SingleOrDefault();
            return View(u);

        }

        // GET: users/Create
        public ActionResult Create()
        {


            //ViewBag.admin_id = new SelectList(db.admins, "admin_id", "username");
            ViewBag.catalog_id = new SelectList(db.catalogs, "id", "name");
            return View();
        }


        [HttpPost]

        public ActionResult Create(user u, HttpPostedFileBase img)
        {

            //u.confirm_password = FormsAuthentication.HashPasswordForStoringInConfigFile(u.password, "MD5");
            foreach (var item in db.users.ToList())
            {
                if (item.mail == u.mail)
                {
                    TempData["mail"] = "E-mail is Exit";
                    return RedirectToAction("Create");
                }
                else if (item.username == u.username && item.password == u.password)
                {
                    TempData["mail"] = "change password";
                    return RedirectToAction("Create");
                }

            }

            if (img == null)
            {
                u.photo = "/photos/an.jpeg";
            }
            else
            {
               
                img.SaveAs(Server.MapPath($"~/photos/{img.FileName}"));
                u.photo = $"/photos/{img.FileName}";
            }
            //u.password = FormsAuthentication.HashPasswordForStoringInConfigFile(u.password, "MD5"); ;
            u.admin_id = 1;
            u.state = "wait";
            u.like_id = 0;
            if (ModelState.IsValid)
            {
                db.users.Add(u);
                db.SaveChanges();
                return RedirectToAction("login", "Home");
            }


            return View(u);
        }


      

        //// GET: users/Edit/5
        //public ActionResult Edit()
        //{
        //    if (Session["id"] == null) return RedirectToAction("login", "Home");

        //    int id = (int)Session["id"];

        //    user user = db.users.Find(id);

        //    ViewBag.admin_id = new SelectList(db.admins, "admin_id", "username", user.admin_id);
        //    ViewBag.catalog_id = new SelectList(db.catalogs, "id", "name", user.catalog_id);
        //    return View(user);
        //}

        //// POST: users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id,user user )
        //{
        //    if (Session["id"] == null) return RedirectToAction("login", "Home");


        //    if (ModelState.IsValid)
        //    {

        //        var u = db.users.Where(n => n.id == id).SingleOrDefault();
        //        u.username = user.username;
        //        u.state = user.state;
        //        u.address = user.address;
        //        u.age = user.age;





        //        db.SaveChanges();
        //        return RedirectToAction("profile");
        //    }


        //    ViewBag.admin_id = new SelectList(db.admins, "admin_id", "username", user.admin_id);
        //    ViewBag.catalog_id = new SelectList(db.catalogs, "id", "name", user.catalog_id);
        //    return View(user);
        //}


        public ActionResult like()
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");
            int id = (int)Session["id"];


            List<like> like = db.likes.Where(n => n.user_id == id).ToList();
            ViewBag.likecount = like.Count();

            return View(like);
        }
        public ActionResult comment()
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");
            int id = (int)Session["id"];


            List<comment> comment = db.comments.Where(n => n.user_id == id).ToList();

            ViewBag.commentcount = comment.Count();
            return View(comment);
        }
        public ActionResult Deletecomment(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");
            var comm = db.comments.Where(n => n.id_user == id).FirstOrDefault();

            db.comments.Remove(comm);
            db.SaveChanges();
            return RedirectToAction("comment");
        }
        public ActionResult Deletelike(int id)
        {
            if (Session["id"] == null) return RedirectToAction("login", "Home");
            var like = db.likes.Where(n => n.id_user == id).FirstOrDefault();

            db.likes.Remove(like);
            db.SaveChanges();
            return RedirectToAction("like");
        }
        //public ActionResult photo()
        //{
        //    if (Session["id"] == null) return RedirectToAction("login", "Home");
        //    int id = (int)Session["id"];
        //    var pho = db.users.Where(n => n.id == id).Select(n => n.photo).SingleOrDefault();
        //    ViewBag.photo = pho;
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult photo(HttpPostedFileBase img)
        //{


        //    if (Session["id"] == null) return RedirectToAction("login", "Home");
        //    int id = (int)Session["id"];
        //    var u = db.users.Where(n => n.id == id).SingleOrDefault();
        //    if (img == null)
        //    {
        //        u.photo = "/photos/an.jpeg";
        //    }
        //    else
        //    {
        //        img.SaveAs(Server.MapPath($"~/photos/{img.FileName}"));
        //        u.photo = $"/photos/{img.FileName}";
        //    }


        //    db.SaveChanges();
        //    return RedirectToAction("profile");



        //}
        //public ActionResult password()
        //{
        //    if (Session["id"] == null) return RedirectToAction("login", "Home");

        //    return View();
        //}

        //public ActionResult password2(string password)
        //{
        //    if (Session["id"] == null) return RedirectToAction("login", "Home");

        //    int id = (int)Session["id"];
        //    var user = db.users.Find(id);
        //    if (user.password == password)
        //    {
        //        return View();
        //    }
        //    else return RedirectToAction("password");
        //}
        //public ActionResult password3(string password)
        //{
        //    if (Session["id"] == null) return RedirectToAction("login", "Home");

        //    int id = (int)Session["id"];
        //    user user = db.users.Find(id);
        //    user.password = password;
        //    db.SaveChanges();
        //    return RedirectToAction("login", "Home");
        //}

    }
}
