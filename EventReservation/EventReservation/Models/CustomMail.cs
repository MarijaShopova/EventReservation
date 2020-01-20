using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class CustomMail
    {
            public string To { get; set; }
            public string From = "karachanakova98@gmail.com";
            public string Subject = "Confrming for your reservation";
            public string Body { get; set; }
        }

    }
