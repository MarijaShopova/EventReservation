using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class EventFiltersViewModel
    {
        public String city { get; set; }
        public int localId { get; set; }
        public String genre { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}