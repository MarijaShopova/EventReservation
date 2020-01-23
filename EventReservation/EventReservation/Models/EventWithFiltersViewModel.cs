using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class EventWithFiltersViewModel
    {
        public List<Event> events { get; set; }
        public List<Local> locals { get; set; }
        public List<String> cities { get; set; }
        public List<String> sorting { get; set; }
        public List<String> genres { get; set; }
        public EventFiltersViewModel filters { get; set; }

        public EventWithFiltersViewModel()
        {
            sorting = new List<string>();
            genres = new List<string>();
            sorting.Add("newest events");
            genres.Add("Rock");
            genres.Add("Pop");
            genres.Add("Techno");
            genres.Add("Balkan");
            genres.Add("Hip Hop");
            genres.Add("XY hits");
        }
    }
}