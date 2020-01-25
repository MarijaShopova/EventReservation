using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models
{
    public class LocalImage
    {
        [Key]
        public int Id { get; set; }
        public int LocalId { get; set; }
        public Local Local { get; set; }
        public byte[] Image { get; set; }
    }
}