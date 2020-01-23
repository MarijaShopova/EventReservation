using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace EventReservation.Models
{
    public class EventWithFiltersViewModel
    {
        public List<Event> events { get; set; }
        public List<Local> locals { get; set; }
        public List<String> cities { get; set; }
        public List<String> genres { get; set; }
        public EventFiltersViewModel filters { get; set; }

        public EventWithFiltersViewModel()
        {
            genres = new List<string>();
            genres.Add("Rock");
            genres.Add("Pop");
            genres.Add("Techno");
            genres.Add("Balkan");
            genres.Add("Hip Hop");
            genres.Add("XY hits");
        }
    }
}