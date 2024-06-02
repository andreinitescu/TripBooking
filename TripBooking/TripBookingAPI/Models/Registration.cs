using System.ComponentModel.DataAnnotations;

namespace TripBookingAPI.Models
{
	public class Registration
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int TripId { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
