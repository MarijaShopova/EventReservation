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
        [Display(Name = "Free tables")]
        public int FreeTables { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        [Required]
        public string Genre { get; set; }
        public int LocalId { get; set; }
        public Local local { get; set; }
    }
}