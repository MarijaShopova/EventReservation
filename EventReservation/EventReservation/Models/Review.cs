﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Range(0,5)]
        public int Stars { get; set; }
        public int localId { get; set; }
        public Local Local { get; set; }
        public string User { get; set; } //user email
    }
}