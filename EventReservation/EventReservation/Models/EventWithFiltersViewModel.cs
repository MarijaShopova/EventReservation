using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class EventWithFiltersViewModel
    {
        public List<Event> events { get; set; }
        List<Local> locals;
        List<String> cities;
        List<String> sorting;
        List<String> genres;

        EventWithFiltersViewModel()
        {
            cities = (List<String>) locals.Select(m => m.City).Distinct();

        }
    }
}