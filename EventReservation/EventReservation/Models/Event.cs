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
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        public Boolean Ticket { get; set; }
        public int TicketPrice { get; set; }
        [Required]
        public int FreeTables { get; set; }
        public string BandName { get; set; }
        [Required]
        public string Genre { get; set; }
        public int LocalId { get; set; }
        public Local local { get; set; }
    }
}