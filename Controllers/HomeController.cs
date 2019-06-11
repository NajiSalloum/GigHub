using GigHub.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                                g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }

            
            List<GigViewModel> gigsList = new List<GigViewModel>();
            foreach (var item in upcomingGigs)
            {

                var attendance = _context.Attendances
                    .Where(a => a.AttendeeId == userId && a.GigId == item.Id)
                    .ToList()
                    .Count();
                var Going = false;
                if (attendance > 0)
                {
                    Going = true;
                }

                var following = _context.Followings
                    .Where(f => f.FollowerId == userId && f.FolloweeId == item.ArtistId)
                    .ToList()
                    .Count();
                var Followed = false;
                if (following > 0)
                {
                    Followed = true;
                }

                gigsList.Add(new GigViewModel()
                {
                    Artist = item.Artist,
                    ArtistId = item.ArtistId,
                    Attendances = item.Attendances,
                    DateTime = item.DateTime,
                    Genre = item.Genre,
                    GenreId = item.GenreId,
                    Id = item.Id,
                    IsCanceled = item.IsCanceled,
                    Venue = item.Venue,
                    IsGoing = Going,
                    IsFollowing = Followed
                         
                });

            }

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigsList,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query
            };
            return View("Gigs",viewModel);
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
    }
}