using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.Interfaces;
using TripBookingAPI.Models;
using TripBookingAPI.Models.Dtos;

namespace TripBookingAPI.Services
{
	public class TripService : ITripService
	{
		private readonly TripContext _context;

		public TripService(TripContext context)
		{
			_context = context;
		}

		public async Task<Trip> CreateTripAsync(Trip trip)
		{
			if (await _context.Trips.AnyAsync(t => t.Name == trip.Name))
			{
				throw new ArgumentException("Trip name must be unique.");
			}

			_context.Trips.Add(trip);
			await _context.SaveChangesAsync();

			return trip;
		}

		public async Task<bool> UpdateTripAsync(int id, Trip trip)
		{
			if (id != trip.Id)
			{
				throw new ArgumentException("Trip ID mismatch.");
			}

			_context.Entry(trip).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
				return true;
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await TripExistsAsync(id))
				{
					return false;
				}
				else
				{
					throw;
				}
			}
		}

		public async Task<bool> DeleteTripAsync(int id)
		{
			var trip = await _context.Trips.FindAsync(id);
			if (trip is null)
			{
				return false;
			}

			_context.Trips.Remove(trip);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
		{
			return await _context.Trips
				.Select(t => new TripDto { Name = t.Name, Country = t.Country, StartDate = t.StartDate })
				.ToListAsync();
		}

		public async Task<Trip> GetTripByIdAsync(int id)
		{
			var trip = await _context.Trips.FindAsync(id);
			if (trip is null)
			{
				return null;
			}

			return trip;
		}

		public async Task<IEnumerable<TripDto>> SearchTripsByCountryAsync(string country)
		{
			return await _context.Trips
				.Where(t => t.Country.Equals(country, StringComparison.OrdinalIgnoreCase))
				.Select(t => new TripDto { Name = t.Name, Country = t.Country, StartDate = t.StartDate })
				.ToListAsync();
		}

		private async Task<bool> TripExistsAsync(int id)
		{
			return await _context.Trips.AnyAsync(e => e.Id == id);
		}
	}
}
