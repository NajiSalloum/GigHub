using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class GigViewModel
    {
        public int Id { get; set; }

        public bool IsCanceled { get;  set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get;  set; }

        public bool IsGoing{ get; set; }

        public bool  IsFollowing { get; set; }
    }
}