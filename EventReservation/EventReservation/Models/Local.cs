﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        public string Manager { get; set; }
        public string Description { get; set; }
        [Required]
        public string City { get; set; }
        [Display(Name = "Stret")]
        public string StreetName { get; set; }
        [Range(0, 99999, ErrorMessage = "Please enter a valid number.")]
        public int StreetNo { get; set; }
        [Display(Name = "Opens")]
        public DateTime OpeningHour { get; set; } = DateTime.Parse("Jan 1, 2000");
        [Display(Name = "Closes")]
        public DateTime ClosingHour { get; set; } = DateTime.Parse("Jan 1, 2000");
        public Boolean Parking { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<LocalImage> LocalImages { get; set; }

        public byte[] LocalsImage { get; set; }

        public Local()
        {
            Events = new List<Event>();
            Reviews = new List<Review>();
            LocalImages = new List<LocalImage>();
        }
    }
}