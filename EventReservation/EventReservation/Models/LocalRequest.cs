using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventReservation.Models
{
    public class LocalRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String LocalName { get; set; }
        public String LocalCity { get; set; }

        [Column(TypeName = "image")]
        public byte[] LocalsImage { get; set; }

    }
}