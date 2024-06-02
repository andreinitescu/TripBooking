namespace TripBookingAPI.Models.Dtos
{
	public class TripDetailDto : TripDto
	{
		public string Description { get; set; }
		public int NumberOfSeats { get; set; }
	}
}
