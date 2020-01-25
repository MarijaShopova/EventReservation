using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class Reservation
    {
        [Key, Column(Order = 0)]
        public string Name { get; set; }

        [Key, Column(Order = 1)]
        public int eventId { get; set; }
        public virtual Event Event { get; set; }
        public int NoTables { get; set; }
        public string userEmail { get; set; }
        public Event Event { get; set; }
    }
}