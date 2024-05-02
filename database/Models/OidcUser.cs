using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    public class OidcUser
    {
        public enum CredentialTypeLookup : ushort
        {
            None = 0,
            Siteminder = 1,
            KeycloakBceid = 2,
            BcWalletLawyer = 3
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public CredentialTypeLookup CredentialType { get; set; }

        [Required]
        public string UniqueIdentifier { get; set; } = null!;

        public ICollection<BookingHistory> Bookings { get; } = new List<BookingHistory>();
        public DateTime? LastLogin { get; set; }
    }
}
