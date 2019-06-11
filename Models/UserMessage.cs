using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class UserMessage
    {
        [Key]
        [Column(Order = 1)]
        public int MessageId { get;  set; }

        public Message Message { get;  set; }

        [Key]
        [Column(Order = 2)]
        public string SenderId { get;  set; }

        public ApplicationUser Sender { get;  set; }

        [Key]
        [Column(Order = 3)]
        public string ReceiverId { get;  set; }

        public ApplicationUser Receiver { get;  set; }

        public bool IsRead { get;  set; }

    }
}