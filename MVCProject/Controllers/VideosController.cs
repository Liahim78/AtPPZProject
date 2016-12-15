using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL.Context;
using DAL.Entity;
using Microsoft.AspNet.Identity;

namespace MVCProject.Controllers
{
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index()
        {
            string id= User.Identity.GetUserId();
            var v = from video in db.Videos
                    where video.IsNew && video.MyUser.Id ==id
                    orderby video.Likes.Count descending
                    select video;
            return View(v.ToList());
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
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

        // GET: Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UrlVideoContent,IsPublic,IsNew,Discription,DateTime,MainId")] Video video)
        {
            if (ModelState.IsValid)
            {
                string id = User.Identity.GetUserId();
                video.MyUser = (from u in db.Users
                                where u.Id == id
                                select u).SingleOrDefault();
                video.DateTime = DateTime.Now;
                video.IsNew = true;
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Videos/Delete/5
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

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
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

        public ActionResult OldVideo(int id)
        {
            var v = from video in db.Videos
                    where video.MainId == id
                    orderby video.Likes.Count descending
                    select video;
            return View(v.ToList());
        }
    }
}
