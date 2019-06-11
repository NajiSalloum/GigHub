using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FollowingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Followings
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var followings = db.Followings
                .Where(f => f.FollowerId == userId)
                .ToList();
            List<FollowingView> followingList = new List<FollowingView>();
            foreach (var item in followings)
            {
                var Followed = db.Users.FirstOrDefault(u => u.Id == item.FolloweeId);
                followingList.Add(new FollowingView()
                {
                    Id = item.FolloweeId,
                    Name = Followed.Name,
                    ImagePath = Followed.ImagePath
                });
            }    

            return View(followingList);
        }

        // GET: Followings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Following following = db.Followings.Find(id);
            if (following == null)
            {
                return HttpNotFound();
            }
            return View(following);
        }

             public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Following following = db.Followings.Find(id);
            if (following == null)
            {
                return HttpNotFound();
            }
            return View(following);
        }

        // POST: Followings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Following following = db.Followings.Find(id);
            db.Followings.Remove(following);
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
    }
}
