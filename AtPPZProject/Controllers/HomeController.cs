using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using AtPPZProject.Models;
using DAL.Entity;
using Microsoft.AspNet.Identity;

namespace AtPPZProject.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var v = from video in db.Videos
                             where video.IsNew && video.MyUser.Id==User.Identity.GetUserId()
                             orderby video.Likes.Count descending
                             select video;
                return View("Home",v);
            }
            var videos = from video in db.Videos
                where video.IsPublic && video.IsNew
                orderby video.Likes.Count descending
                select video;
            return View(videos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            var Video = db.Videos.Find(id);
            return View(Video);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var video = db.Videos.Find(id);
            return View(video);
        }
        [HttpPost]
        public ActionResult Edit(Video video)
        {
            if (ModelState.IsValid)
            {
                var oldVideo = db.Videos.Find(video.Id);
                oldVideo.UrlVideoContent = video.UrlVideoContent;
                oldVideo.Discription = video.Discription;
                oldVideo.IsPublic = video.IsPublic;
                db.Entry(oldVideo).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("Index");
            }
            return View(video);
        }

        public ActionResult OldVideos(int id)
        {
            var videos = from v in db.Videos
                where v.MainId == id
                orderby v.Likes.Count descending
                select v;
            return View(videos);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Video video)
        {
            if (ModelState.IsValid)
            {
                video.MyUser = (from u in db.Users
                                where u.Id == User.Identity.GetUserId()
                               select u).SingleOrDefault();
                video.DateTime = DateTime.Now;
                video.IsNew = true;
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user);
        }
        [HttpPost]
        public ActionResult EditProfile(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = db.Users.Find(user.Id);
                oldUser.FirsName = user.FirsName;
                oldUser.LastName = user.LastName;
                oldUser.Birthday = user.Birthday;
                oldUser.City = user.City;
                oldUser.Country = user.Country;
                oldUser.Info = user.Info;
                oldUser.Nick = user.Nick;
                oldUser.Phone = user.Phone;
                oldUser.Stiles = user.Stiles;
                db.Entry(oldUser).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("Index");
            }
            return View(user);
        }
    }
}