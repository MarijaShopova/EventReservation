using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }    
        [Required]
        [Display(Name ="Begins")]
        public DateTime DateStart { get; set; }
        [Required]
        [Display(Name = "Ends")]
        public DateTime DateEnd { get; set; }
        [Display(Name = "Has the event ticket?")]
        public Boolean Ticket { get; set; }
        [Display(Name = "Ticket price")]
        public int TicketPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Tables")]
        public int NoTables { get; set; }
        [Required]
        [Display(Name = "Reserved tables")]
        public int ReservedTables { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        [Required]
        public string Genre { get; set; }
        public int LocalId { get; set; }
        public Local local { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public Event()
        {
            Reservations = new List<Reservation>();
        }
    }
}