using TripBookingAPI.Models;

namespace TripBookingAPI.Interfaces
{
	public interface IRegistrationService
	{
		Task<Registration> CreateRegistrationAsync(Registration registration);
		Task<IEnumerable<Registration>> GetAllRegistrationsAsync();
		Task<Registration> GetRegistrationByIdAsync(int id);
	}
}
