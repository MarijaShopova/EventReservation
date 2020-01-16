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
        [Display(Name ="Date")]
        public DateTime DateStart { get; set; }
        [Required]
        [Display(Name = "Time Starts")]
        public DateTime TimeStart { get; set; }
        [Required]
        [Display(Name = "Time Ends")]
        public DateTime TimeEnd { get; set; }
        [Display(Name = "Has the event a ticket?")]
        public Boolean HasTicket { get; set; }
        [Display(Name = "Ticket price")]
        public int TicketPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Number of free tables")]
        public int NoTables { get; set; }
        public int ReservedTables { get; set; }
        [Required]
        [Display(Name = "Performer")]
        public string Performer { get; set; }
        [Required]
        [Display(Name = "Music Genre")]
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