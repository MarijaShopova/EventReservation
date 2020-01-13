using System;
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
        [Required]
        public string Manager { get; set; }
        public string Description { get; set; }
        [Required]
        public string City { get; set; }
        [Display(Name = "Stret")]
        public string StreetName { get; set; }
        public int StreetNo { get; set; }
        [Display(Name = "Opens")]
        public DateTime OpeningHour { get; set; } = DateTime.Now;
        [Display(Name = "Closes")]
        public DateTime ClosingHour { get; set; } = DateTime.Now;
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