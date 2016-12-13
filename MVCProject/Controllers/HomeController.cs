using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DAL.Context;
using Microsoft.AspNet.Identity;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db= new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index","Videos");
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
                string id = User.Identity.GetUserId();
                var oldUser = db.Users.Find(id);
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