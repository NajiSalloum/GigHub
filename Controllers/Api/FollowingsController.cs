using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
            {
                var rFollowing = _context.Followings.Single(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
                _context.Followings.Remove(rFollowing);
                _context.SaveChanges();
                return BadRequest("The attendance is already exists.");
            }
               
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId

            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}
