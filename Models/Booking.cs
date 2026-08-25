using System.ComponentModel.DataAnnotations;

namespace SpaRelaxApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string ServiceType { get; set; } = string.Empty; // T.ex. Massage, Ansiktsbehandling

        [Required]
        public DateTime BookingDate { get; set; }

        // Koppling till användaren
        public string UserId { get; set; } = string.Empty;
    }
}