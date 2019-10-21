﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class Local
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string City { get; set; }
        [Display(Name = "Stret")]
        public string StreetName { get; set; }
        public int StreetNo { get; set; }
        [Display(Name = "Opens")]
        public DateTime OpeningHour { get; set; }
        [Display(Name = "Closes")]
        public DateTime ClosingHour { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Tables")]
        public int NoTables { get; set; }
        public Boolean Parking { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Local ()
        {
            Events = new List<Event>();
            Reviews = new List<Review>();
        }
    }
}