using System.ComponentModel.DataAnnotations;

namespace TripBookingAPI.Models
{
	public class Trip
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[MaxLength(20)]
		public string Country { get; set; }

		public string Description { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Range(1, 100)]
		public int NumberOfSeats { get; set; }
	}
}
