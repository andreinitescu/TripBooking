using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.Interfaces;
using TripBookingAPI.Models;

namespace TripBookingAPI.Services
{
	public class RegistrationService : IRegistrationService
	{
		private readonly TripContext _context;

		public RegistrationService(TripContext context)
		{
			_context = context;
		}

		public async Task<Registration> CreateRegistrationAsync(Registration registration)
		{
			if (await _context.Registrations.AnyAsync(r => r.TripId == registration.TripId && r.Email == registration.Email))
			{
				throw new ArgumentException("This email is already registered for the trip.");
			}

			_context.Registrations.Add(registration);
			await _context.SaveChangesAsync();

			return registration;
		}

		public async Task<IEnumerable<Registration>> GetAllRegistrationsAsync()
		{
			return await _context.Registrations.ToListAsync();
		}

		public async Task<Registration> GetRegistrationByIdAsync(int id)
		{
			var registration = await _context.Registrations.FindAsync(id);
			if (registration is null)
			{
				return null;
			}

			return registration;
		}
	}
}