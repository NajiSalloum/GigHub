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
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.GigId == dto.GigId && a.AttendeeId == userId))
            {
                var attend = _context.Attendances.Single(a => a.GigId == dto.GigId && a.AttendeeId == userId);

                _context.Attendances.Remove(attend);
                _context.SaveChanges();
                return BadRequest("The attendance is already exists.");
            }
                
            var attendace = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId

            };
            _context.Attendances.Add(attendace);
            _context.SaveChanges();
            return Ok();
        }
    }
}
